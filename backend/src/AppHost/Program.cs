using AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedisContainer("redis");
var postgres = builder.AddPostgresContainer(name: "postgres", port: 5432, password: "new123!");

var productsDb = postgres.AddDatabase("ProductsDB");
var reviewsDb = postgres.AddDatabase("ReviewsDB");

var basketHost = builder.AddProject<Projects.Basket>("basket-host")
    .WithReference(redis);

var productsHost = builder.AddProject<Projects.Products>("products-host")
    .WithReference(productsDb)
    .WithHttpEndpoint(hostPort: 59095, name:"products-host");

var reviewsHost = builder.AddProject<Projects.Reviews>("reviews-host")
    .WithReference(reviewsDb);

builder.AddPnpmApp("storefront-web", "../../../frontend", "dev", ["--filter=@shopfusion/storefront-web"])
    .WithReference(productsHost);

var gateway = builder.AddProject<Projects.Gateway>("gateway")
    .WithReference(productsHost);

gateway.WithEnvironment("CallBackUrl", gateway.GetEndpoint("https"));

builder.Build().Run();
