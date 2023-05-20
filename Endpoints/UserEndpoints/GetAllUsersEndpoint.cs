using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

[HttpGet("user")]
[AllowAnonymous]
public class GetAllUsersEndpoint : EndpointWithoutRequest<WithError<GetAllUsersResponse>>
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
            await SendAsync(new WithError<GetAllUsersResponse>
            {
                Success = false,
                ErrorMessage = result.Error.Message
            }, 404, ct);
            return;
        }

        var userResponse = result.Data.ToUserResponse();
        await SendOkAsync(new WithError<GetAllUsersResponse>
        {
            Success = true,
            Data = userResponse,
        }, ct);
    }
}