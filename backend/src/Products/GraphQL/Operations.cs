using Mediator;
using ShopFusion.Products.Data;
using ShopFusion.Products.Models;

namespace ShopFusion.Products.GraphQL;

public static class Operations
{
    #region ProductBrands
    
    [Query]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<ProductBrand> GetProductBrands(ProductsDbContext dbContext) => dbContext.ProductBrands;
    
    [Query]
    [NodeResolver]
    public static async Task<ProductBrand?> GetProductBrandById(
        Guid id,
        ProductBrandByIdDataLoader productBrandById,
        CancellationToken cancellationToken)
        => await productBrandById.LoadAsync(id, cancellationToken);
    
    [Mutation]
    public static async Task<ProductBrand?> CreateProductBrandAsync(
        [Service] IMediator mediator,
        CreateProductBrandInput input,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);
    
    [Subscription]
    [Subscribe, Topic(nameof(OnCreatedProductBrand))]
    public static OnCreatedProductBrandPayload OnCreatedProductBrand(
        ProductsDbContext dbContext,
        [EventMessage] ProductBrand productBrand,
        CancellationToken cancellationToken
    )
    {
        return new OnCreatedProductBrandPayload(productBrand);
    }
    
    #endregion 
}
