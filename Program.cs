using salutaris.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointDefinitions(typeof(UserEndpoints));
var app = builder.Build();
app.UseEndpointDefinitions();

app.Run();