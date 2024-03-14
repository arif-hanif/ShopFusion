using HotChocolate.Subscriptions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Data;
using ShopFusion.Products.GraphQL;
using ShopFusion.Products.Models;
using ShopFusion.Shared.Errors;

namespace ShopFusion.Products.Commands;

public class CreateProductBrand(
    IDbContextFactory<ProductsDbContext> dbContextFactory,
    ITopicEventSender eventSender)
    : IRequestHandler<CreateProductBrandInput, ProductBrand?>
{
    public async ValueTask<ProductBrand?> Handle(
        CreateProductBrandInput request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidNameException(nameof(ProductBrand));
        }
        
        await using ProductsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var productBrand = new ProductBrand
        {
            Id = Guid.NewGuid(), 
            Name = request.Name
        };

        try
        {
            await dbContext.ProductBrands.AddAsync(productBrand, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new DatabaseOperationException();
        }

        await eventSender.SendAsync(nameof(Operations.OnCreatedProductBrand), productBrand,
            cancellationToken);

        return productBrand;
    }
}

