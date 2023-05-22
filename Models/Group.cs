namespace salutaris.Models;

public class Group : BaseModel
{
    public List<User> Users { get; set; } = new();
    public List<Expense> Expenses { get; set; } = new();
    public string GroupName { get; set; } = default!;

}