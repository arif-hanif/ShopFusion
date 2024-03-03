using Microsoft.EntityFrameworkCore;

namespace ShopFusion.Products.Data;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}