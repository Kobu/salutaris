#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class GetLoggedInUserRequest
{
    [FromClaim("UserId")] public Guid UserId { get; init; } = default;
}