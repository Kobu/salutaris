using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

[HttpPost("user")]
[AllowAnonymous]
// TODO check EndpointWithMapper
public class CreateUserEndpoint : Endpoint<CreateUserRequest, WithError<UserResponse>>
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
        if (result.IsErr)
        {
            await SendAsync(new WithError<UserResponse>
            {
                Success = false,
                ErrorMessage = result.Error.Message
            }, 404, ct);
            return;
        }

        var userResponse = result.Data.ToUserResponse();
        await SendOkAsync(new WithError<UserResponse>
        {
            Success = true,
            Data = userResponse
        }, ct);
    }
}