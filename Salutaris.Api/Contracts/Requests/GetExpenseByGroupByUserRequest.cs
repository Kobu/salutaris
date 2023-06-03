using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetExpenseByGroupByUserRequest
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    [FromClaim("UserId")]
    public Guid InvokingUser { get; set; }
}