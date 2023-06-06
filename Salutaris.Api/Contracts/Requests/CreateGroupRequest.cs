using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class CreateGroupRequest
{
    public required string Name { get; init; } = default!;
    [FromClaim("UserId")]
    public Guid CreatorId { get; init; } = default!;
}