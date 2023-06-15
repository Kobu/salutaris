#region

using FastEndpoints;

#endregion

namespace salutaris.Contracts.Requests;

public class CreateExpenseRequest
{
    public required Guid GroupId { get; init; }
    [FromClaim("UserId")] public Guid UserId { get; init; } = default;
    public required string Item { get; init; }
    public required decimal Price { get; init; }
    public required string Currency { get; init; }
}