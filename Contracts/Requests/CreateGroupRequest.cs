namespace salutaris.Contracts.Requests;

public class CreateGroupRequest
{
    public string Name { get; init; } = default!;
    public Guid CreatorId { get; init; } = default!;
}