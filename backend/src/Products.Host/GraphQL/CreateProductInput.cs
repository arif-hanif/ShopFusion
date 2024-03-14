using Mediator;
using ShopFusion.Products.Models;

namespace ShopFusion.Products.GraphQL;

public sealed record CreateProductInput(
    string Name,
    string? Description,
    double Price,
    Guid ProductTypeId,
    Guid ProductBrandId
    ) : IRequest<Product?>;
