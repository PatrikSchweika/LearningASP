using LearningASP.Model;

namespace Data.Repositories;

public interface IRecipeRepository
{
    public IEnumerable<Recipe> GetAll();
}