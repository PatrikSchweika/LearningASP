using System.IdentityModel.Tokens.Jwt;

using LearningASP.DTO.Auth;
using LearningASP.Model;

namespace LearningASP.Services;

public interface IAuthService
{
    public User? Register(RegisterDto registerDto);
    public string? Login(LoginDto loginDto);

    public void Logout();

    public User? GetCurrentUser();
}