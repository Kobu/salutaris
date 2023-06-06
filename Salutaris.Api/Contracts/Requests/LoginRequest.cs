namespace salutaris.Contracts.Requests;

public class LoginRequest
{
    public required Guid UserId { get; init; } = default;
    public required string Password { get; init; } = default!;
}