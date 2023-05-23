using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Services;
using EmptyResponse = FastEndpoints.EmptyResponse;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpPost("group/{id:guid}/join")]
[AllowAnonymous]
public class JoinGroupEndpoint : ResultEndpoint<JoinGroupRequest,EmptyResponse>
{
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public JoinGroupEndpoint(IGroupService groupService, IUserService userService)
    {
        _groupService = groupService;
        _userService = userService;
    }

    protected override async Task<bool> HandleResult(JoinGroupRequest req)
    {
        var user = await _userService.GetUserById(req.UserId);
        if (user.IsErr)
        {
            return await HandleErr(user);
        }

        var group = await _groupService.GetGroupById(req.GroupId);
        if (group.IsErr)
        {
            return await HandleErr(group);
        }

        var result = await _groupService.JoinGroup(group.Data, user.Data);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        return await HandleOk(new());
    }
}