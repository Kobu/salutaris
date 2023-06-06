using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetExpenseByGroupByUserRequest
{
    public required Guid UserId { get; init; } = default;
    public required Guid GroupId { get; init; } = default;
    [FromClaim("UserId")] public  Guid InvokingUser { get; init; } = default;
}