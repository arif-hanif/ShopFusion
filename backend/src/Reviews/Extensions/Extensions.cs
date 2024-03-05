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
            ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            IDbContextFactory<ReviewsDbContext> dbContextFactory =
                scope.ServiceProvider.GetRequiredService<IDbContextFactory<ReviewsDbContext>>();
            using ReviewsDbContext dbContext = dbContextFactory.CreateDbContext();
            dbContext.Database.Migrate();
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
