namespace salutaris.Models;

public class Group : BaseModel
{
    public User Creator { get; set; } = default!;
    public Guid CreatorId { get; set; }
    public ICollection<UserGroup> UserGroups { get; set; }
    public List<Expense> Expenses { get; set; } = new();
    public string GroupName { get; set; } = default!;
}