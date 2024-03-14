using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Data;

namespace ShopFusion.Products.Host.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddPooledDbContextFactory<ProductsDbContext>(
                x => x.UseNpgsql(builder.Configuration.GetConnectionString("Products")));
        
        //if (builder.Environment.IsDevelopment())
        //{
        //    builder.Services.AddMigration<ProductsDbContext>();
        //}
        
        builder.Services
            .AddMediator();
        
        builder.Services
            .AddGraphQLServer()
            .AddTypes()
            .AddInMemorySubscriptions()
            .AddGlobalObjectIdentification()
            .AddMutationConventions()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<ProductsDbContext>(DbContextKind.Pooled)
            .AddInstrumentation(o => o.RenameRootActivity = true);
    }
}
