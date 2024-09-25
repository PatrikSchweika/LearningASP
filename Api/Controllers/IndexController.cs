using LearningASP.Options;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningASP.Controllers;

[ApiController]
[Route("")]
public class IndexController : ControllerBase
{
    private readonly AppConfiguration _configuration;
    private readonly ILogger<IndexController> _logger;

    public IndexController(IOptions<AppConfiguration> options, ILogger<IndexController> logger)
    {
        _configuration = options.Value;
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        _logger.LogInformation("Get started");

        return _configuration.WelcomeMessage;
    }
}