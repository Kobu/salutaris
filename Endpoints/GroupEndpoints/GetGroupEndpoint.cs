using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpGet("group/{id:guid}")]
[AllowAnonymous]
public class GetGroupEndpoint : Endpoint<GetGroupByIdRequest, WithError<GroupResponse>>
{


    private readonly IGroupService _groupService;

    public GetGroupEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public override async Task HandleAsync(GetGroupByIdRequest req, CancellationToken ct)
    {
        var result = await _groupService.GetGroupById(req.Id);
        if (result.IsErr)
        {
            await SendAsync(new WithError<GroupResponse>
            {
                Success = false,
                ErrorMessage = result.Error.Message
            }, 404, ct);
            return;
        }

        var response = result.Data.ToResponse();
        await SendOkAsync(new WithError<GroupResponse>
        {
            Success = true,
            Data = response
        }, ct);
    }
}