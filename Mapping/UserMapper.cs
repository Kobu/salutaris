using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Models;

namespace salutaris.Mapping;

public static class UserMapper
{
    public static User ToUser(this CreateUserRequest request)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Name = request.Name
        };
    }

    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            id = user.Id,
            Name = user.Name,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}