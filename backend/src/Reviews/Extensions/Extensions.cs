using EntityFramework.Helpers;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Data;

namespace ShopFusion.Reviews.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddPooledDbContextFactory<ReviewsDbContext>(
                x => x.UseNpgsql(builder.Configuration.GetConnectionString("ReviewsDB")));

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigration<ReviewsDbContext>();
        }
        
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
