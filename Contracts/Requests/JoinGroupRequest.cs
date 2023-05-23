using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class JoinGroupRequest
{
    public Guid UserId { get; init; } = default!;

    [BindFrom("id")] public Guid GroupId { get; init; } = default!;
}