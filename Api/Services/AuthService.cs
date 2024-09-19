using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Data.Repositories;

using LearningASP.Controllers;
using LearningASP.DTO.Auth;
using LearningASP.Model;
using LearningASP.Options;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearningASP.Services;

public class AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IOptions<JwtConfiguration> options) : IAuthService
{
    private readonly JwtConfiguration _configuration = options.Value;


    private string GenerateSalt()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        return Convert.ToBase64String(salt);
    }

    public User? Register(RegisterDto registerDto)
    {
        var existingUser = userRepository.GetByEmail(registerDto.Email);

        if (existingUser != null)
        {
            return null;
        }

        var salt = GenerateSalt();

        var newUser = new User
        {
            Id = 0,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Salt = salt,
            PasswordHash = "",
            Recipes = []
        };

        var passwordHash = passwordHasher.HashPassword(newUser, registerDto.Password);

        newUser = newUser with { PasswordHash = passwordHash, Salt = salt };

        return userRepository.Create(newUser);
    }

    public string? Login(LoginDto loginDto)
    {
        var user = userRepository.GetByEmail(loginDto.Email);

        if (user == null)
        {
            return null;
        }

        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new(AppClaimTypes.UserId, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Role, "user"),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey)),
                SecurityAlgorithms.HmacSha256)
            );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return serializedToken;
    }

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public User? GetCurrentUser(HttpContext context)
    {
        var parsed = int.TryParse(context.User.FindFirst(AppClaimTypes.UserId)?.Value ?? "", out int userId);

        if (!parsed)
        {
            return null;
        }

        var user = userRepository.GetById(userId);

        return user;
    }
}