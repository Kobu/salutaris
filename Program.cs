using FastEndpoints;
using salutaris.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGroupService, GroupService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();
var app = builder.Build();
app.UseFastEndpoints();

app.Run();