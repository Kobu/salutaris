#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class CreateGroupRequest
{
    public required string Name { get; init; }

    [FromClaim("UserId")] public Guid CreatorId { get; init; } = default!;
}