using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Data;
using ShopFusion.Products.Models;

namespace ShopFusion.Products.GraphQL;

[ExtendObjectType<Product>]
public static class ProductNode
{
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, Product>> GetProductByIdAsync(
        IReadOnlyList<Guid> ids,
        IDbContextFactory<ProductsDbContext> dbContextFactory,
        CancellationToken cancellationToken)
    {
        await using ProductsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Products
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
}
