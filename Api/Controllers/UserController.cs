using LearningASP.DTO;
using LearningASP.Model;
using LearningASP.Options;
using LearningASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningASP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly UserControllerConfiguration _configuration;

        public UserController(IUserService userService, ILogger<UserController> logger, IOptions<UserControllerConfiguration> options) 
        {
            _userService = userService;
            _logger = logger;
            _configuration = options.Value;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            _logger.LogInformation("Get all users");

            if (_configuration.ReturnDummyUsers)
            {
                return
                [
                    new User(69, "Pepa", "Smith")
                ];
            }
            
            return _userService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User?> Get(int id)
        { 
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public User Create([FromBody] CreateUserDto user)
        {
            return _userService.Add(user);
        }

        [HttpPut]
        public User? Replace([FromBody] PutUserDto user)
        {
            return _userService.Update(user);
        }

        [HttpPatch("{id}")]
        public User? Patch(int id, [FromBody] PatchUserDto userPatch)
        {
            return _userService.Patch(id, userPatch);
        }

        [HttpDelete("{id}")]
        public ActionResult<User?> Delete(int id)
        {
            var user = _userService.Remove(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
