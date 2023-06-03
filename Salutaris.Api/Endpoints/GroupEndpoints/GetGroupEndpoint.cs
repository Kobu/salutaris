using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.GroupEndpoints;

public class GetGroupEndpoint : ResultEndpoint<GetGroupByIdRequest,GroupResponse>
{
    private readonly IGroupService _groupService;

    public override void Configure()
    {
        Get("group/{id:guid}");
        AllowAnonymous();
    }

    public GetGroupEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }

    protected override async Task<bool> HandleResult(GetGroupByIdRequest req)
    {
        var result = await _groupService.GetGroupById(req.Id);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var response = result.Data.ToResponse();
        return await HandleOk(response);
    }
}