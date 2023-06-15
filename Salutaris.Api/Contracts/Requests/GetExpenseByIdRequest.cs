#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class GetExpenseByIdRequest
{
    public required Guid Id { get; init; }
    [FromClaim] public string UserId { get; init; } = default!;
}