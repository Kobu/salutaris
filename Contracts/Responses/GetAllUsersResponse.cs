using salutaris.Models;

namespace salutaris.Contracts.Responses;

public class GetAllUsersResponse
{
    public IEnumerable<UserResponse> Users { get; init; } = Enumerable.Empty<UserResponse>();
}