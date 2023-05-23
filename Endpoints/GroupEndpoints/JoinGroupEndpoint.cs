using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Services;
using EmptyResponse = FastEndpoints.EmptyResponse;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpPost("group/{id:guid}/join")]
[AllowAnonymous]
public class JoinGroupEndpoint : Endpoint<JoinGroupRequest, WithError<EmptyResponse>>
{
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public JoinGroupEndpoint(IGroupService groupService, IUserService userService)
    {
        _groupService = groupService;
        _userService = userService;
    }

    public override async Task HandleAsync(JoinGroupRequest req, CancellationToken ct)
    {
        var user = await _userService.GetUserById(req.UserId);
        if (user.IsErr)
        {
            await SendAsync(new()
            {
                Success = false,
                ErrorMessage = user.Error.Message
            } ,404, ct);
            return;
        }

        var group = await _groupService.GetGroupById(req.GroupId);
        if (group.IsErr)
        {
            await SendAsync(new()
            {
                Success = false,
                ErrorMessage = group.Error.Message
            } ,404, ct);
            return;
        }

        var result = await _groupService.JoinGroup(group.Data, user.Data);
        if (result.IsErr)
        {
            await SendAsync(new()
            {
                Success = false,
                ErrorMessage = result.Error.Message
            } ,404, ct);
            return;
        }

        await SendOkAsync(new()
        {
            Success = true,
        }, ct);

    }
}