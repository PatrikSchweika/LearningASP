using LearningASP.Model;

namespace LearningASP.Services;

public interface IRecipeService
{
    public IEnumerable<Recipe> GetAll();
}