using LearningASP.Model;
using LearningASP.Services;

using Microsoft.AspNetCore.Mvc;

namespace LearningASP.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController(IRecipeService service) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Recipe> Get()
    {
        return service.GetAll();
    }

    // [HttpPost]
    // public User Create([FromBody] CreateUserDto user)
    // {
    //     return _userService.Add(user);
    // }
    //
    // [HttpPut]
    // public User? Replace([FromBody] PutUserDto user)
    // {
    //     return _userService.Update(user);
    // }
    //
    // [HttpPatch("{id}")]
    // public User? Patch(int id, [FromBody] PatchUserDto userPatch)
    // {
    //     return _userService.Patch(id, userPatch);
    // }
    //
    // [HttpDelete("{id}")]
    // public ActionResult<User?> Delete(int id)
    // {
    //     var user = _userService.Remove(id);
    //
    //     if (user == null) return NotFound();
    //
    //     return user;
    // }
}