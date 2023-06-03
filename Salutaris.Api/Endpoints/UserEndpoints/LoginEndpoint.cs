using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authorization;
using salutaris.Contracts.Requests;
using salutaris.Contracts.Responses;
using salutaris.Services;
using salutaris.Utils;

namespace salutaris.Endpoints.UserEndpoints;

public class LoginEndpoint : ResultEndpoint<LoginRequest, AuthenticationResponse>
{
    public override void Configure()
    {
        Post("user/login");
        AllowAnonymous();
    }

    private readonly IUserService _userService;
    private readonly IAuthorizationService _authorizationService;

    public LoginEndpoint(IUserService userService, IAuthorizationService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
    }

    protected override async Task<bool> HandleResult(LoginRequest req)
    {
        var isAuthenticated = await _userService.AuthenticateUser(req.UserId, req.Password);
        if (isAuthenticated.IsErr)
        {
            return await HandleErr(isAuthenticated);
        }

        if (!isAuthenticated.Data)
        {
            return await HandleErr("Invalid credentials", 401);
        }
        
        
        var jwtToken = JWTBearer.CreateToken(
            signingKey: "TokenSigningKeyw",
            expireAt: DateTime.UtcNow.AddDays(1),
            priviledges: u =>
            {
                u.Claims.Add(new("UserId", req.UserId.ToString()));
            });
        
        return await HandleOk(new AuthenticationResponse
        {
            UserId = req.UserId,
            Token = jwtToken
        });
    }
}