using ShopFusion.Products.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseWebSockets();
app.MapGet("/", () => "Welcome to the products subgraph!");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
