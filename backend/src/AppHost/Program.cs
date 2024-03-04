using AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder
    .AddRedisContainer("redis");

var postgres = builder
    .AddPostgresContainer(name: "postgres", port: 5432, password: "new123!");

var productsDb = postgres.AddDatabase("ProductsDB");
var reviewsDb = postgres.AddDatabase("ReviewsDB");

var productsHost = builder
    .AddProject<Projects.Products>("products-host")
    .WithReference(productsDb)
    .WithHttpEndpoint(hostPort: 59090, name: "products");

var basketHost = builder
    .AddProject<Projects.Basket>("basket-host")
    .WithReference(redis)
    .WithHttpEndpoint(hostPort: 59091, name: "basket");

var reviewsHost = builder
    .AddProject<Projects.Reviews>("reviews-host")
    .WithReference(reviewsDb)
    .WithHttpEndpoint(hostPort: 59092, name: "reviews");

builder
    .AddPnpmApp(
        name: "storefront-web",
        workingDirectory:"../../../frontend",
        scriptName: "dev", 
        args: ["--filter=@shopfusion/storefront-web"])
    .WithReference(productsHost);

builder
    .AddProject<Projects.Gateway>(name: "gateway")
    .WithReference(productsHost)
    .WithReference(basketHost)
    .WithReference(reviewsHost);

builder.Build().Run();
