using ShopFusion.Products.Host.Extensions;
using ShopFusion.ServiceDefaults;
using IronWord;
using IronWord.Models;
using IronSoftware.Drawing;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseWebSockets();
app.MapGet("/", () => "Welcome to the products subgraph!");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
