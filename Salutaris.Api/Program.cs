using FastEndpoints;
using FastEndpoints.Security;
using salutaris.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddCors();
builder.Services.AddJWTBearerAuth("TokenSigningKeyw"); 

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGroupService, GroupService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();
// builder.Services.AddJWTBearerAuth("TokenSigningKeyw");
var app = builder.Build();
app.UseFastEndpoints();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseAuthentication(); //add this
app.UseAuthorization();
app.Run();