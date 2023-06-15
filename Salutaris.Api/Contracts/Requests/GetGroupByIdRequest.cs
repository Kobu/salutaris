#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class GetGroupByIdRequest
{
    public required Guid Id { get; init; }
    [FromClaim] public Guid UserId { get; init; } = default;
}