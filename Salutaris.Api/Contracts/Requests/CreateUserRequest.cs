namespace salutaris.Contracts.Requests;

public class CreateUserRequest
{
    public required string Name { get; init; }
    public required string Password { get; init; }
}