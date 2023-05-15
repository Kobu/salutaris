using FastEndpoints;
using salutaris.Endpoints;
using salutaris.Models;
using salutaris.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();

builder.Services.AddSingleton<IUserService, UserService>();
var app = builder.Build();
app.UseFastEndpoints();

app.Run();