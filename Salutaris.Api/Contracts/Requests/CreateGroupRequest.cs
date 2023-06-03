using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class CreateGroupRequest
{
    public string Name { get; init; } = default!;
    [FromClaim]
    public Guid CreatorId { get; init; } = default!;
}