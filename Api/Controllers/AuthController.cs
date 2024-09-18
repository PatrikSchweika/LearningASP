using LearningASP.DTO.Auth;
using LearningASP.Model;
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
    public ActionResult Login([FromBody] LoginDto loginDto)
    {
        var token = authService.Login(loginDto);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody] RegisterDto registerDto)
    {
        var user = authService.Register(registerDto);

        if (user == null)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("logout")]
    public async Task Logout()
    {
        // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}