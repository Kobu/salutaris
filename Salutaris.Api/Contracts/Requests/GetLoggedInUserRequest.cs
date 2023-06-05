using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetLoggedInUserRequest
{
    [FromClaim("UserId")]
    public Guid UserId { get; init; } = default;
}