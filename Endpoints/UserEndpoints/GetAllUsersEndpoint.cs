using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

[HttpGet("user")]
[AllowAnonymous]
public class GetAllUsersEndpoint : EndpointWithoutRequest<GetAllUsersResponse>
{
    private readonly IUserService _userService;

    public GetAllUsersEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _userService.GetAllUsers();
        if (result.IsErr)
        {
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var userResponse = result.Data.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}