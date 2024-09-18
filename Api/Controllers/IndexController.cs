﻿using LearningASP.Options;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningASP.Controllers;

[ApiController]
[Route("")]
public class IndexController : ControllerBase
{
    private readonly AppConfiguration _configuration;

    public IndexController(IOptions<AppConfiguration> options)
    {
        _configuration = options.Value;
    }

    [HttpGet]
    public string Get()
    {
        return _configuration.WelcomeMessage;
    }
}