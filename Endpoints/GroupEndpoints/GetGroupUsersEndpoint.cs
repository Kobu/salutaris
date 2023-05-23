using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpGet("group/{id:guid}/users")]
[AllowAnonymous]
public class GetGroupUsersEndpoint : Endpoint<GetGroupByIdRequest,WithError<List<UserResponse>>>
{
    private readonly IGroupService _groupService;

    public GetGroupUsersEndpoint(IGroupService groupService)
    {
        _groupService = groupService;
    }


    public override async Task HandleAsync(GetGroupByIdRequest req,CancellationToken ct)
    {
        var result = await _groupService.GetGroupById(req.Id);
        if (result.IsErr)
        {
            await SendAsync(new()
            {
                Success = false,
                ErrorMessage = result.Error.Message
            }, 404, ct);
            return;
        }

        var response = result.Data.Users
            .Select(user => user.ToUserResponse())
            .ToList();
        
        await SendOkAsync(new()
        {
            Success = true,
            Data = response
        }, ct);
        
    }
}