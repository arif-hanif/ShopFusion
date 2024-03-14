using ShopFusion.Reviews.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor();

builder.AddApplicationServices();

var app = builder.Build();

app.UseWebSockets();
app.MapGet("/", () => "Welcome to the reviews subgraph!");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
