using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Infrastructure;
using ShopFusion.Products.Domain;

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
