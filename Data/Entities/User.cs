using System.ComponentModel.DataAnnotations;

namespace LearningASP.Model;

public record User(int Id, string FirstName, string LastName);