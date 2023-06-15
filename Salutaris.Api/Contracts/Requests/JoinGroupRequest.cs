#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class JoinGroupRequest
{
    public required Guid UserId { get; init; }

    [BindFrom("id")] public Guid GroupId { get; init; } = default;
}