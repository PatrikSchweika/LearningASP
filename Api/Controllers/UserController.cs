using LearningASP.DTO;
using LearningASP.Model;
using LearningASP.Options;
using LearningASP.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningASP.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        _logger.LogInformation("Get all users");

        return _userService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<User?> Get(int id)
    {
        var user = _userService.GetById(id);

        if (user == null) return NotFound();

        return user;
    }
}