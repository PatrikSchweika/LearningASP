using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

public class AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IHttpContextAccessor accessor, IOptions<JwtConfiguration> options) : IAuthService
{
    private readonly JwtConfiguration _configuration = options.Value;

    public User? Register(RegisterDto registerDto)
    {
        var existingUser = userRepository.GetByEmail(registerDto.Email);

        if (existingUser != null)
        {
            return null;
        }

        var newUser = new User
        {
            Id = 0,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PasswordHash = passwordHasher.HashPassword(null, registerDto.Password),
            Recipes = []
        };

        return userRepository.Create(newUser);
    }

    public string? Login(LoginDto loginDto)
    {
        var passwordHash = passwordHasher.HashPassword(null, loginDto.Password);

        var user = userRepository.GetByEmailAndPasswordHash(loginDto.Email, passwordHash);

        if (user == null)
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

    public User? GetCurrentUser()
    {
        if (accessor.HttpContext == null)
        {
            return null;
        }

        var parsed = int.TryParse(accessor.HttpContext.User.FindFirst(AppClaimTypes.UserId).Value, out int userId);

        if (!parsed)
        {
            return null;
        }

        var user = userRepository.GetById(userId);

        return user;
    }
}