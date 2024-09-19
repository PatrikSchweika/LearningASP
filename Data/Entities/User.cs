namespace LearningASP.Model;

// (int Id, string Email, string Password, string FirstName, string LastName, ICollection<Recipe> Recipes);

public record User
{
    public int Id { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public string Salt { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public ICollection<Recipe> Recipes { get; init; }
}