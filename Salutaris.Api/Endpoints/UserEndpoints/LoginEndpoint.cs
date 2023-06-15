#region

using System.Security.Claims;
using FastEndpoints.Security;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Services;

#endregion

namespace salutaris.Endpoints.UserEndpoints;

public class LoginEndpoint : ResultEndpoint<LoginRequest, AuthenticationResponse>
{
    private readonly IUserService _userService;

    public LoginEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("user/login");
        AllowAnonymous();
    }

    protected override async Task<bool> HandleResult(LoginRequest req)
    {
        var user = await _userService.AuthenticateUserByUsername(req.Username, req.Password);
        if (user.IsErr)
        {
            return await HandleErr(user);
        }

        var jwtToken = JWTBearer.CreateToken(
            "TokenSigningKeyw",
            expireAt: DateTime.UtcNow.AddDays(1),
            priviledges: u =>
            {
                u.Claims.Add(new Claim("UserId", user.Data.Id.ToString()));
                u.Claims.Add(new Claim("Username", user.Data.Name));
            });

        return await HandleOk(new AuthenticationResponse
        {
            UserId = user.Data.Id,
            Token = jwtToken,
            Username = user.Data.Name
        });
    }
}