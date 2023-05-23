using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpGet("group")]
[AllowAnonymous]
public class GetAllGroupsEndpoint : EndpointWithoutRequest<WithError<List<GroupResponse>>>
{
    private readonly IGroupService _groupService;

    public GetAllGroupsEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _groupService.GetAllGroups();
        if (result.IsErr)
        {
            await SendAsync(new WithError<List<GroupResponse>>
            {
                Success = false,
                ErrorMessage = result.Error.Message
            }, 404, ct);
            return;
        }

        var response = result.Data
            .Select(x => x.ToResponse())
            .ToList();
        
        await SendOkAsync(new WithError<List<GroupResponse>>
        {
            Success = true,
            Data = response
        }, ct);
    }
}