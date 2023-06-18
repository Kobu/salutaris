using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetUserRequest
{
    [BindFrom("username")]
    public required string Username { get; init; }
}