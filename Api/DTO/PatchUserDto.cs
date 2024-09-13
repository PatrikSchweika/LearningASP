using System.ComponentModel.DataAnnotations;

namespace LearningASP.DTO;

public record PatchUserDto
{
    [StringLength(100, MinimumLength = 4)]
    public string? FirstName { get; init; }

    [StringLength(100, MinimumLength = 4)]
    public string? LastName { get; init; }
}