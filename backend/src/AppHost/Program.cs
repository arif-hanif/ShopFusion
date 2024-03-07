using AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder
    .AddRedisContainer("redis");

var postgres = builder
    .AddPostgresContainer(name: "postgres", port: 5432, password: "new123!");

var productsDb = postgres.AddDatabase("ProductsDB");
var reviewsDb = postgres.AddDatabase("ReviewsDB");

var products = builder
    .AddProject<Projects.Products>("products")
    .WithReference(productsDb);

var basket = builder
    .AddProject<Projects.Basket>("basket")
    .WithReference(redis);

var reviews = builder
    .AddProject<Projects.Reviews>("reviews")
    .WithReference(reviewsDb);

var gateway = builder
    .AddFusionGateway<Projects.Gateway>(name: "gateway")
    .WithSubgraph(products)
    .WithSubgraph(reviews);
    //.WithSubgraph(basket);

var gatewayEndpoint = gateway.GetEndpoint("http");

builder
    .AddPnpmApp(
        name: "storefront-web",
        workingDirectory: "../../../frontend",
        scriptName: "dev",
        args: ["--filter=@shopfusion/storefront-web"])
    .WithEnvironment("GRAPHQL_ENDPOINT", gatewayEndpoint.UriString);

builder
    .Build()
    .Compose()
    .Run();
