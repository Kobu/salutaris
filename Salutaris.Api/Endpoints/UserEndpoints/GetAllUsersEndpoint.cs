﻿using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

public class GetAllUsersEndpoint : ResultEndpointWithoutRequest<GetAllUsersResponse>
{
    private readonly IUserService _userService;

    
    public override void Configure()
    {
        Get("user");
        AllowAnonymous();
    }

    public GetAllUsersEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task<bool> HandleResult()
    {
        var result = await _userService.GetAllUsers();
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var userResponse = result.Data.ToUserResponse();
        return await HandleOk(userResponse);
    }

}