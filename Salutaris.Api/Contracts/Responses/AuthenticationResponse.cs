namespace salutaris.Contracts.Responses;

public class AuthenticationResponse
{
    public required Guid UserId { get; set; }
    public required string Token { get; set; }
    public required string Username { get; set; }
}