namespace salutaris.Models;

public class User : BaseModel
{
    public required string Name { get; init; }

    public List<UserGroup> UserGroups { get; init; } = new();

    public List<Expense> Expenses { get; init; } = new();

    public required string Password { get; init; } = default!;
}