using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Infrastructure;
using ShopFusion.ServiceDefaults;

namespace ShopFusion.Reviews.Host.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<ReviewsDbContext>("Reviews");
        
        builder.Services
            .AddPooledDbContextFactory<ReviewsDbContext>(
                x => x.UseNpgsql(builder.Configuration.GetConnectionString("Reviews")));
        
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
            .RegisterDbContext<ReviewsDbContext>(DbContextKind.Pooled)
            .AddInstrumentation(o => o.RenameRootActivity = true);
    }
}
