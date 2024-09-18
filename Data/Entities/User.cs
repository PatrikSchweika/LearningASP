using System.ComponentModel.DataAnnotations;

namespace LearningASP.Model;

public record User(int Id, string Email, string Password, string FirstName, string LastName);