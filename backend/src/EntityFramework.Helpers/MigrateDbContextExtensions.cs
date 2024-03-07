using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EntityFramework.Helpers;

public static class MigrateDbContextExtensions
{
    public static IServiceCollection AddMigration<TContext>(this IServiceCollection services)
        where TContext : DbContext
        => services.AddMigration<TContext>((_, _) => Task.CompletedTask);

    public static IServiceCollection AddMigration<TContext>(
        this IServiceCollection services, 
        Func<TContext, IServiceProvider, Task> seeder)
        where TContext : DbContext
    {
        return services.AddHostedService(sp => new MigrationHostedService<TContext>(sp, seeder));
    }

    public static IServiceCollection AddMigration<TContext, TDbSeeder>(this IServiceCollection services)
        where TContext : DbContext
        where TDbSeeder : class, IDbSeeder<TContext>
    {
        services.AddScoped<IDbSeeder<TContext>, TDbSeeder>();
        return services.AddMigration<TContext>((context, sp) => sp.GetRequiredService<IDbSeeder<TContext>>().SeedAsync(context));
    }

    private static async Task MigrateDbContextAsync<TContext>(this IServiceProvider services, Func<TContext, IServiceProvider, Task> seeder) where TContext : DbContext
    {
        
        using IServiceScope scope = services.CreateScope();
        var scopeServices = scope.ServiceProvider;
        
        IDbContextFactory<TContext> dbContextFactory = scopeServices.GetRequiredService<IDbContextFactory<TContext>>();
        
        TContext dbContext = await dbContextFactory.CreateDbContextAsync();
    
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(() => InvokeSeeder(seeder, dbContext, scopeServices));
        
    }

    private static async Task InvokeSeeder<TContext>(
        Func<TContext, IServiceProvider, Task> seeder, 
        TContext context, 
        IServiceProvider services)
        where TContext : DbContext
    {
        await context.Database.MigrateAsync();
        await seeder(context, services);
    }

    private class MigrationHostedService<TContext>(
        IServiceProvider serviceProvider, 
        Func<TContext, IServiceProvider, Task> seeder)
        : BackgroundService where TContext : DbContext
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return serviceProvider.MigrateDbContextAsync(seeder);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}

