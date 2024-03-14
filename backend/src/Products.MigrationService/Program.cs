using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Infrastructure;
using ShopFusion.Products.MigrationService;
using ShopFusion.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<ProductsDbContext>("Products", null,
    optionsBuilder => optionsBuilder.UseNpgsql(x =>
        x.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<Worker>());

var app = builder.Build();

//app.MapDefaultEndpoints();

await app.RunAsync();
