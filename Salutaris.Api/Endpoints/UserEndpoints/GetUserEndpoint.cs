#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.UserEndpoints;

public class GetUserEndpoint : ResultEndpoint<GetUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public GetUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Get("user/{username}");
    }

    protected override async Task<bool> HandleResult(GetUserRequest req)
    {
        var result = await _userService.GetUserByName(req.Username);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        if (result.Data is null)
        {
            return await HandleErr($"User of name {req.Username} not found");
        }

        var userResponse = result.Data.ToUserResponse();
        return await HandleOk(userResponse);
    }
}