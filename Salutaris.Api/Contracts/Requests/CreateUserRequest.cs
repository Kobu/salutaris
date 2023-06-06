namespace salutaris.Contracts.Requests;

public class CreateUserRequest
{
    public required string Name { get; init; } = default!;
    public required string Password { get; init; } = default!;
}