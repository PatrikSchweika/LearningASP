using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace LearningASP.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SecretController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        var claims = HttpContext.User.Claims;

        var claimsStr = "";

        foreach (var claim in claims)
        {
            claimsStr += $"{claim.Type}: {claim.Value}\n";
        }

        return claimsStr;
    }
}