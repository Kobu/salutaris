namespace salutaris.Contracts.Responses;

public class AuthenticationResponse
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
}