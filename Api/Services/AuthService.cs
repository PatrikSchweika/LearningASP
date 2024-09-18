using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using LearningASP.Options;

using Microsoft.IdentityModel.Tokens;

namespace LearningASP.Services;

public class AuthService(IUserService userService, IHttpContextAccessor contextAccessor, JwtConfiguration configuration) : IAuthService
{
    public string? Login(string email, string password)
    {
        var user = userService.GetByEmailAndPassword(email, password);

        if (user == null)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Role, "user"),
        };

        var token = new JwtSecurityToken(
            issuer: configuration.Issuer,
            audience: configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretKey)),
                SecurityAlgorithms.HmacSha256)
            );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return serializedToken;
    }

    public void Logout()
    {
        throw new NotImplementedException();
    }
}