namespace salutaris.Contracts.Responses;

public class WithError<T>
{
    public required bool Success { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
}