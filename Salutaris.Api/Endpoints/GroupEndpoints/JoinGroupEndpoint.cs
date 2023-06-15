#region

using FastEndpoints;
using salutaris.Contracts.Requests;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.GroupEndpoints;

public class JoinGroupEndpoint : ResultEndpoint<JoinGroupRequest, EmptyResponse>
{
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public JoinGroupEndpoint(IGroupService groupService, IUserService userService)
    {
        _groupService = groupService;
        _userService = userService;
    }

    public override void Configure()
    {
        Claims("UserId");
        Post("group/{id:guid}/join");
    }

    protected override async Task<bool> HandleResult(JoinGroupRequest req)
    {
        var user = await _userService.GetUserById(req.UserId);
        if (user.IsErr)
        {
            return await HandleErr(user);
        }

        // TODO move this to the service layer
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

        return await HandleOk(new EmptyResponse());
    }
}