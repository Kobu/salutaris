namespace salutaris.Contracts.Responses;

public class ExpenseResponse
{
    public required Guid Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required Guid GroupId { get; init; }
    public required Guid UserId { get; init; }
    public required string Item { get; init; }
    public required decimal Price { get; init; }
    public required string Currency { get; init; }
}

public class ExpenseResponseFull : ExpenseResponse
{
    public required string UserName { get; init; }
    public required string GroupName { get; init; }
}