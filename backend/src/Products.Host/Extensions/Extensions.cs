using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Infrastructure;
using ShopFusion.ServiceDefaults;

namespace ShopFusion.Products.Host.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services
            .AddPooledDbContextFactory<ProductsDbContext>(
                x => x.UseNpgsql(builder.Configuration.GetConnectionString("Products")));
        
        
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
