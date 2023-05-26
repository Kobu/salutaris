namespace salutaris.Models;

public class Expense : BaseModel
{
    public required string Item { get; init; } = default!;
    public required decimal Price { get; init; }
    public required string Currency { get; init; } = default!;
    public required Guid UserId { get; init; }
    public required User User { get; init; } = default!;
    public required Guid GroupId { get; init; }
    public required Group Group { get; init; } = default!;
}