using FastEndpoints;
using salutaris.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGroupService, GroupService>();
var app = builder.Build();
app.UseFastEndpoints();

app.Run();