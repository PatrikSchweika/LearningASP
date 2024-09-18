namespace LearningASP.Model;

// (int Id, User User, string Title, string Description);

public record Recipe
{
    public int Id { get; init; }
    public User User { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
}