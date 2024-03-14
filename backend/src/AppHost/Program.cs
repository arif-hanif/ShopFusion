var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("ShopFusion")
    .WithPgAdmin();

var productsDb = postgres
    .AddDatabase(name: "Products");
var reviewsDb = postgres
    .AddDatabase(name: "Reviews");

var products = builder
    .AddProject<Projects.ShopFusion_Products_Host>("products-host")
    .WithReference(productsDb);

builder
    .AddProject<Projects.ShopFusion_Products_MigrationService>("products-migrations")
    .WithReference(productsDb);

var reviews = builder
    .AddProject<Projects.ShopFusion_Reviews_Host>("reviews-host")
    .WithReference(reviewsDb);

builder.AddProject<Projects.ShopFusion_Reviews_MigrationService>("reviews-migrations")
    .WithReference(reviewsDb);

builder
    .AddFusionGateway<Projects.ShopFusion_Gateway_Host>(name: "gateway-host")
    .WithSubgraph(products)
    .WithSubgraph(reviews);

builder
    .Build()
    .Compose()
    .Run();
