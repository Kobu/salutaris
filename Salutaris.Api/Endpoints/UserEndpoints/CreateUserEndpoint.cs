#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.UserEndpoints;

public class CreateUserEndpoint : ResultEndpoint<CreateUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public CreateUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("user");
        AllowAnonymous();
    }

    protected override async Task<bool> HandleResult(CreateUserRequest req)
    {
        var user = req.ToUser();
        var result = await _userService.CreateNewUser(user);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var userResponse = result.Data.ToUserResponse();
        return await HandleOk(userResponse);
    }
}