using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Models;
using salutaris.Repositories;
using salutaris.Services;
using Group = salutaris.Models.Group;

namespace salutaris.Endpoints.GroupEndpoints;

[HttpPost("/group")]
[AllowAnonymous]
public class CreateGroupEndpoint : Endpoint<CreateGroupRequest, WithError<GroupResponse>>
{
    private readonly IGroupService _groupService ;
    private readonly IUserService _userService;

    public CreateGroupEndpoint(IGroupService groupService, IUserService userService)
    {
        _groupService = groupService;
        _userService = userService;
    }

    public override async Task HandleAsync(CreateGroupRequest req, CancellationToken ct)
    {
        var userExists = await _userService.GetUserById(req.CreatorId);
        if (userExists.IsErr)
        {
            await SendAsync(new WithError<GroupResponse>
            {
                Success = false,
                ErrorMessage = userExists.Error.Message
            }, 404, ct);
            return;
        }

        var group = new Group
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Creator = userExists.Data,
            GroupName = req.Name
        };

        var result = await _groupService.CreateGroup(group);
        if (result.IsErr)
        {
            await SendAsync(new WithError<GroupResponse>
            {
                Success = false,
                ErrorMessage = result.Error.Message
            }, 404, ct);
            return;        }

        await SendOkAsync(new WithError<GroupResponse>
        {
            Success = true,
            Data = result.Data.ToResponse(),
        }, ct);
    }
}