#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class GetGroupByIdRequest
{
    [BindFrom("id")]
    public required Guid Id { get; init; }
    [FromClaim] public Guid UserId { get; init; } = default;
}