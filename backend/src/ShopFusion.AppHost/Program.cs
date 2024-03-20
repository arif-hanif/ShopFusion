var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("psql-server")
    .WithPgAdmin();

var productsDb = postgres
    .AddDatabase(name: "Products");

var productsHost = builder
    .AddProject<Projects.ShopFusion_Products_Host>("products-host")
    .WithReference(productsDb);

builder
    .AddProject<Projects.ShopFusion_Products_MigrationService>("products-migrations")
    .WithReference(productsDb);

var reviewsDb = postgres
    .AddDatabase(name: "Reviews");

var reviewsHost = builder
    .AddProject<Projects.ShopFusion_Reviews_Host>("reviews-host")
    .WithReference(reviewsDb);

builder
    .AddProject<Projects.ShopFusion_Reviews_MigrationService>("reviews-migrations")
    .WithReference(reviewsDb);

builder.AddFusionGateway<Projects.ShopFusion_Gateway_Host>("gateway-host")
    .WithSubgraph(productsHost)
    .WithSubgraph(reviewsHost);

builder
    .Build()
    .Compose()
    .Run();
