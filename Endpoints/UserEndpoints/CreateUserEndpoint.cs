using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

[HttpPost("user")]
[AllowAnonymous]
public class CreateUserEndpoint : ResultEndpoint<CreateUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public CreateUserEndpoint(IUserService userService)
    {
        _userService = userService;
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