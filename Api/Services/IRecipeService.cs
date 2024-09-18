using LearningASP.DTO.Recipe;
using LearningASP.Model;

namespace LearningASP.Services;

public interface IRecipeService
{
    public IEnumerable<Recipe> GetAll();
    public IEnumerable<Recipe> GetUserRecipes();
    public Recipe Create(CreateRecipeDto recipeDto);
}