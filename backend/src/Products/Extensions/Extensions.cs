using ShopFusion.Products.Data;

namespace ShopFusion.Products.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ProductsDbContext>("ProductsDB");
        
        if(builder.Environment.IsDevelopment())
            builder.Services.AddMigration<ProductsDbContext>();
    }
}
