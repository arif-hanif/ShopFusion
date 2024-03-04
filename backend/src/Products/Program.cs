using ShopFusion.Products.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();

builder.Services
    .AddHttpContextAccessor();

var app = builder.Build();

app.UseWebSockets();
app.MapGet("/", () => "Welcome to the products host!");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
