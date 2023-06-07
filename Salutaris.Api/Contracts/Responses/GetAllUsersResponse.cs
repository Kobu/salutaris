namespace salutaris.Contracts.Responses;

public class GetAllUsersResponse
{
    public required IEnumerable<UserResponse> Users { get; init; } 
}