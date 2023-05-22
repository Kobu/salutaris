namespace salutaris.Models;

public class User : BaseModel
{
    public string Name { get; init; } = default!;
    
    public List<Group> Groups { get; set; } = new();
    
}