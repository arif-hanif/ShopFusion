using Mediator;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Products.Infrastructure;
using ShopFusion.Products.GraphQL;
using ShopFusion.Products.Domain;
using ShopFusion.Shared.Errors;

namespace ShopFusion.Products.Commands;

public class CreateProduct(
    IDbContextFactory<ProductsDbContext> dbContextFactory)
    //ITopicEventSender eventSender)
    : IRequestHandler<CreateProductInput, Product?>
{
    public async ValueTask<Product?> Handle(
        CreateProductInput request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidNameException(nameof(Product));
        }
        
        await using ProductsDbContext dbContext = 
            await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var product = new Product
        {
            Id = Guid.NewGuid(), 
            Name = request.Name,
            ProductBrandId = request.ProductBrandId,
            ProductTypeId = request.ProductTypeId
        };

        try
        {
            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new DatabaseOperationException();
        }
        
        /*
        await eventSender.SendAsync(
            nameof(Operations.OnCreatedProduct), 
            product,
            cancellationToken);
            
        */

        return product;
    }
}

