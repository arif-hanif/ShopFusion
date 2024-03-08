using AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgresContainer(name: "postgres", port: 5432, password: "new123!");

var productsDb = postgres.AddDatabase("ProductsDB");
var reviewsDb = postgres.AddDatabase("ReviewsDB");

var products = builder
    .AddProject<Projects.Products>("products")
    .WithReference(productsDb);

var reviews = builder
    .AddProject<Projects.Reviews>("reviews")
    .WithReference(reviewsDb);

builder
    .AddFusionGateway<Projects.Gateway>(name: "gateway")
    .WithSubgraph(products)
    .WithSubgraph(reviews);

builder
    .Build()
    .Compose()
    .Run();
