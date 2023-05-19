namespace salutaris.Contracts.Responses;

public class WithError<T>
{
    public required bool Success { get; init; } = default;
    public T? Data { get; init; } = default!;
    public string? ErrorMessage { get; init; } = default;
}