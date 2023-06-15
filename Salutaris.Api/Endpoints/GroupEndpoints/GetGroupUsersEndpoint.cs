#region

using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.GroupEndpoints;

public class GetGroupUsersEndpoint : ResultEndpoint<GetGroupByIdRequest, List<UserResponse>>
{
    private readonly IGroupService _groupService;

    public GetGroupUsersEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public override void Configure()
    {
        Claims("UserId");
        Get("group/{id:guid}/users");
    }


    protected override async Task<bool> HandleResult(GetGroupByIdRequest req)
    {
        var userInGroup = await _groupService.UserBelongsToGroup(req.Id, req.UserId);
        if (userInGroup.IsErr)
        {
            return await HandleErr(userInGroup);
        }

        if (!userInGroup.Data)
        {
            return await HandleErr("This user cannot view this group", StatusCodes.Status401Unauthorized);
        }

        var result = await _groupService.GetGroupUsers(req.Id);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var response = result.Data
            .Select(user => user.ToUserResponse())
            .ToList();

        return await HandleOk(response);
    }
}