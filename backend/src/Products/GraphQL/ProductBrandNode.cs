using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Data;
using ShopFusion.Products.Models;

namespace ShopFusion.Products.GraphQL;

[ExtendObjectType<ProductBrand>]
public static class ProductBrandNode
{
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, ProductBrand>> GetProductBrandByIdAsync(
        IReadOnlyList<Guid> ids,
        IDbContextFactory<ProductsDbContext> dbContextFactory,
        CancellationToken cancellationToken)
    {
        await using ProductsDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.ProductBrands
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
}
