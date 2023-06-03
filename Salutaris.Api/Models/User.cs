namespace salutaris.Models;

public class User : BaseModel
{
    public string Name { get; init; } = default!;

    public ICollection<UserGroup> UserGroups { get; set; }

    public List<Expense> Expenses { get; set; } = new();

    public string Password { get; init; } = default!;
}