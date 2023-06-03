using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.GroupEndpoints;

public class GetAllGroupsEndpoint : ResultEndpointWithoutRequest<List<GroupResponse>>
{
    private readonly IGroupService _groupService;

    public override void Configure()
    {
        Get("group");
        AllowAnonymous();
    }
    
    public GetAllGroupsEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }

    protected override async Task<bool> HandleResult()
    {
        var result = await _groupService.GetAllGroups();
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var response = result.Data
            .Select(x => x.ToResponse())
            .ToList();

        return await HandleOk(response);
    }
}