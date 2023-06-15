#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class GetExpenseByGroupByUserRequest
{
    public required Guid UserId { get; init; }
    public required Guid GroupId { get; init; }
    [FromClaim("UserId")] public Guid InvokingUser { get; init; } = default;
}