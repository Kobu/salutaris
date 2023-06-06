using FastEndpoints;

namespace salutaris.Contracts.Requests;

public class CreateExpenseRequest
{
    public required Guid GroupId { get; init; } = default!;
    [FromClaim("UserId")] public Guid UserId { get; init; } = default!;
    public required string Item { get; init; } = default!;
    public required decimal Price { get; init; } = default!;
    public required string Currency { get; init; } = default!;
}