using System.ComponentModel.DataAnnotations;

namespace LearningASP.DTO.Auth;

public record RegisterDto : BaseUserDto
{
    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; init; }
}