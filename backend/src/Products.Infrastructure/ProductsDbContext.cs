using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Domain;

namespace ShopFusion.Products.Infrastructure;

/// <remarks>
/// Add migrations using the following command inside the 'Products' project directory:
///
/// dotnet ef migrations add --context ProjectsDbContext [migration-name]
/// </remarks>
public class ProductsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProductBrand> ProductBrands => Set<ProductBrand>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<Product> Products => Set<Product>();
}
