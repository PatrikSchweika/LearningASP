using System.ComponentModel.DataAnnotations;

namespace LearningASP.DTO.Recipe;

public class CreateRecipeDto
{
    [Required]
    public string Title { get; init; }
    [Required]
    public string Description { get; init; }
}