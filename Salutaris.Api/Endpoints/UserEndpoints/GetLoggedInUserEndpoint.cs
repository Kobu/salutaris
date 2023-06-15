#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.UserEndpoints;

public class GetLoggedInUserEndpoint : ResultEndpoint<GetLoggedInUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public GetLoggedInUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Get("user/me");
    }

    protected override async Task<bool> HandleResult(GetLoggedInUserRequest req)
    {
        var result = await _userService.GetUserById(req.UserId);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var userResponse = result.Data.ToUserResponse();
        return await HandleOk(userResponse);
    }
}