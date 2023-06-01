namespace salutaris.Contracts.Requests;

public class GetExpenseByIdRequest
{
    public Guid Id { get; init; } = default!;
}