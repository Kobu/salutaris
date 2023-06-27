#region

using FastEndpoints;
using FastEndpoints.Security;
using salutaris;
using salutaris.Services;

#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddCors();
builder.Services.AddJWTBearerAuth("TokenSigningKeyw");

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGroupService, GroupService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();

var app = builder.Build();
app.UseFastEndpoints();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseAuthentication();
app.UseAuthorization();
// new Seeding().SeedData(); //TODO UNCOMMENT THE LINE FOR SEEDING
app.Run();