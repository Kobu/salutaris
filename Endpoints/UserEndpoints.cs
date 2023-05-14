using salutaris.Models;
using salutaris.Repositories;

namespace salutaris.Endpoints;

public class UserEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<UserRepository>();
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/users", (UserRepository repo) => repo.GetAllUsers());
        app.MapGet("/users/{id:guid}", (UserRepository repo, Guid id) => repo.GetUserById(id));
        app.MapPost("/users", (UserRepository repo, UserModel user) => repo.CreateUser(user));
    }
}