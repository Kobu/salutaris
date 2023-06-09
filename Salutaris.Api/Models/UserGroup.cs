namespace salutaris.Models;

public class UserGroup
{
    public required Guid UserId { get; init; }
    public required User User { get; init; }
    public required Guid GroupId { get; init; }
    public required Group Group { get; init; }
}