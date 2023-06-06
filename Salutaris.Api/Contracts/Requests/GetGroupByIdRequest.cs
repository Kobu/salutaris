using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetGroupByIdRequest
{
    public required Guid Id { get; init; } = default;
    [FromClaim] public Guid UserId { get; init; } = default;
}