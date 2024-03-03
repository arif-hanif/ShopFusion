using AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedisContainer("redis");
var postgres = builder.AddPostgresContainer("postgres");

var productsDb = postgres.AddDatabase("ProductsDB");
var reviewsDb = postgres.AddDatabase("ReviewsDB");

var basketHost = builder.AddProject<Projects.Basket>("basket-host")
    .WithReference(redis);

var productsHost = builder.AddProject<Projects.Products>("products-host")
    .WithReference(productsDb);

var reviewsHost = builder.AddProject<Projects.Reviews>("reviews-host")
    .WithReference(reviewsDb);

builder.AddPnpmApp("storefront-web", "../../../frontend", "dev", ["--filter=@shopfusion/storefront-web"])
    .WithReference(productsHost);

builder.Build().Run();
