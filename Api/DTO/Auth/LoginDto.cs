using System.ComponentModel.DataAnnotations;

namespace LearningASP.DTO.Auth;

public record LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }
}