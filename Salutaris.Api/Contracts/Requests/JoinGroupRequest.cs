#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class JoinGroupRequest
{
    [FromClaim("Username")] public string Username { get; init; } = default!;

    [BindFrom("id")] public Guid GroupId { get; init; } = default;
}