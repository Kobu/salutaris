using FastEndpoints;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Mapping;
using salutaris.Services;

namespace salutaris.Endpoints.UserEndpoints;

public class GetUserEndpoint : ResultEndpoint<GetUserRequest, UserResponse>
{
    public override void Configure()
    {
        Get("user/{id:guid}");
    }
    
    private readonly IUserService _userService;

    public GetUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task<bool> HandleResult(GetUserRequest req)
    {
        var result = await _userService.GetUserById(req.Id);
        if (result.IsErr)
        {
            return await HandleErr(result);
        }

        var userResponse = result.Data.ToUserResponse();
        return await HandleOk(userResponse);
    }
}