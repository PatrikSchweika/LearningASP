using System.ComponentModel.DataAnnotations;

namespace LearningASP.DTO;

public record PutUserDto
{
    [Required]
    public required int Id { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public required string FirstName { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public required string LastName { get; init; }
}