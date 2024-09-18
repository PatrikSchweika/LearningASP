using System.ComponentModel.DataAnnotations;

namespace LearningASP.DTO;

public abstract record BaseUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string FirstName { get; init; }

    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string LastName { get; init; }
}