using LearningASP.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LearningASP.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> Login(string email, string password)
    {
        var token = authService.Login(email, password);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task Register()
    {

    }

    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}