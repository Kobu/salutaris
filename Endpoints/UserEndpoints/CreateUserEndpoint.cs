using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

[HttpPost("user"), AllowAnonymous]
public class CreateUserEndpoint : Endpoint<CreateUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public CreateUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var user = req.ToUser();
        var result = await _userService.CreateNewUser(user);
        var userResponse = result.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}