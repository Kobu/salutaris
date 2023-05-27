using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpGet("group/{id:guid}/users")]
[AllowAnonymous]
public class GetGroupUsersEndpoint : ResultEndpoint<GetGroupByIdRequest, List<UserResponse>>
{
    private readonly IGroupService _groupService;

    public GetGroupUsersEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }


    protected override async Task<bool> HandleResult(GetGroupByIdRequest req)
    {
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