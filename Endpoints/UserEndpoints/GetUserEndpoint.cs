using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

[HttpGet("user/{id:guid}")]
[AllowAnonymous]
public class GetUserEndpoint : Endpoint<GetUserRequest, WithError<UserResponse>>
{
    private readonly IUserService _userService;

    public GetUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        var result = await _userService.GetUserById(req.id);
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
            Data = userResponse,
        }, ct);
    }
}