using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningASP.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SecretController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "secret";
    }
}