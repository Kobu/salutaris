namespace salutaris.Contracts.Requests;

public class CreateUserRequest
{
    public string Name { get; init; } = default!;
    public string Password { get; set; }
}