using Aspire.Hosting;
using HotChocolate.Fusion.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

// Databases
var postgres = builder
    .AddPostgres("psql-server")
    .WithPgAdmin();

var productsDb = postgres.AddDatabase(name: "Products");
var reviewsDb = postgres.AddDatabase(name: "Reviews");

// Hosts + Migrations

var productsHost = builder
    .AddProject<Projects.ShopFusion_Products_Host>("products-host")
    .WithReference(productsDb);

builder
    .AddProject<Projects.ShopFusion_Products_MigrationService>("products-migrations")
    .WithReference(productsDb);

var reviewsHost = builder
    .AddProject<Projects.ShopFusion_Reviews_Host>("reviews-host")
    .WithReference(reviewsDb);

builder
    .AddProject<Projects.ShopFusion_Reviews_MigrationService>("reviews-migrations")
    .WithReference(reviewsDb);

// Gateway

builder
    .AddFusionGateway<Projects.ShopFusion_Gateway_Host>("gateway-host")
    .WithOptions(new FusionOptions { EnableGlobalObjectIdentification = true })
    .WithSubgraph(productsHost)
    .WithSubgraph(reviewsHost);

builder
    .Build()
    .Compose()
    .Run();
