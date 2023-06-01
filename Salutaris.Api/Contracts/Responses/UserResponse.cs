namespace salutaris.Contracts.Responses;

public class UserResponse
{
    public Guid id { get; init; }
    public string Name { get; init; } = default!;

    public DateTime CreatedAt { get; init; } = default!;

    public DateTime UpdatedAt { get; init; } = default!;
}