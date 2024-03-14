using HotChocolate.Subscriptions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Infrastructure;
using ShopFusion.Products.GraphQL;
using ShopFusion.Products.Domain;
using ShopFusion.Shared.Errors;

namespace ShopFusion.Products.Commands;

public class CreateProductType(
    IDbContextFactory<ProductsDbContext> dbContextFactory,
    ITopicEventSender eventSender)
    : IRequestHandler<CreateProductTypeInput, ProductType?>
{
    public async ValueTask<ProductType?> Handle(
        CreateProductTypeInput request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidNameException(nameof(ProductType));
        }
        
        await using ProductsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var productType = new ProductType
        {
            Id = Guid.NewGuid(), 
            Name = request.Name
        };

        try
        {
            await dbContext.ProductTypes.AddAsync(productType, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new DatabaseOperationException();
        }

        await eventSender.SendAsync(
            nameof(Operations.OnCreatedProductBrand), 
            productType,
            cancellationToken);

        return productType;
    }
}

