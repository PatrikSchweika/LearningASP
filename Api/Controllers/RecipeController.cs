using System.Security.Claims;

using LearningASP.DTO.Recipe;
using LearningASP.Model;
using LearningASP.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningASP.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RecipeController(IRecipeService recipeService, IAuthService authService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Recipe> GetAll()
    {
        return recipeService.GetAll();
    }

    [HttpGet("User")]
    public ActionResult<IEnumerable<Recipe>> GetUserRecipes()
    {
        var user = authService.GetCurrentUser()!;

        return Ok(user.Recipes.ToList());
    }

    [HttpPost]
    public Recipe Create(CreateRecipeDto recipe)
    {
        return recipeService.Create(recipe);
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