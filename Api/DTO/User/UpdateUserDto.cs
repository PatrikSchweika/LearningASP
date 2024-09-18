using System.ComponentModel.DataAnnotations;

using LearningASP.DTO.Auth;

namespace LearningASP.DTO;

public record UpdateUserDto : RegisterDto
{
    [Required] public required int Id { get; init; }
}