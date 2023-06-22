#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class GetExpenseByGroupByUserRequest
{
    [BindFrom("username")]
    public required string Username { get; init; }
    [BindFrom("groupId")]
    public required Guid GroupId { get; init; }
    // allow only members of the group to view the groups users data
    [FromClaim("Username")] public string InvokingUser { get; init; } = default!;
}