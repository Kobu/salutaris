namespace salutaris.Contracts.Responses;

public class GroupResponse
{
    public required Guid Id { get; init; }
    public required string GroupName { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required Guid CreatorId { get; init; }
} 