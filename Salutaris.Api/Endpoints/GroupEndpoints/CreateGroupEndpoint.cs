#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.GroupEndpoints;

public class CreateGroupEndpoint : ResultEndpoint<CreateGroupRequest, GroupResponse>
{
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public CreateGroupEndpoint(IGroupService groupService, IUserService userService)
    {
        _groupService = groupService;
        _userService = userService;
    }

    public override void Configure()
    {
        Claims("UserId");
        Post("group");
    }

    protected override async Task<bool> HandleResult(CreateGroupRequest req)
    {
        var userExists = await _userService.GetUserById(req.CreatorId);
        if (userExists.IsErr)
        {
            return await HandleErr(userExists);
        }

        var group = req.ToGroup(userExists.Data);
        var result = await _groupService.CreateGroup(group);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        return await HandleOk(result.Data.ToResponse());
    }
}