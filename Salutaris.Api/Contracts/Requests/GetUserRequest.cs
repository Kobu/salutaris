namespace salutaris.Contracts.Requests;

public class GetUserRequest
{
    public required Guid Id { get; init; } = default;
}