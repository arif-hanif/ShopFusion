using Microsoft.EntityFrameworkCore;

namespace ShopFusion.Products.Data;

/// <remarks>
/// Add migrations using the following command inside the 'Products' project directory:
///
/// dotnet ef migrations add --context ProjectsDbContext [migration-name]
/// </remarks>
public class ProductsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}
