namespace salutaris.Contracts.Requests;

public class LoginRequest
{
    public required Guid UserId { get; set; }
    public required string Password { get; set; }
}