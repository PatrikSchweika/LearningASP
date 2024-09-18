using System.IdentityModel.Tokens.Jwt;

namespace LearningASP.Services;

public interface IAuthService
{
    public string? Login(string email, string password);

    public void Logout();
}