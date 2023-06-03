using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class GetExpenseByIdRequest
{
    public Guid Id { get; init; } = default!;
    [FromClaim]
    public string UserId { get; set; }
}