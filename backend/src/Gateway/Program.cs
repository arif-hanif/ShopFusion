var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("Fusion")
    .AddHeaderPropagation();

builder.Services
    .AddFusionGatewayServer()
    .AddServiceDiscoveryRewriter()
    .ConfigureFromFile("./gateway.fgp");

var app = builder.Build();

app.UseWebSockets();
app.UseHeaderPropagation();
app.MapGet("/", () => "Welcome to the gateway!");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
