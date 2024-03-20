using Mediator;
using ShopFusion.Products.Domain;

namespace ShopFusion.Products.GraphQL;

public sealed record CreateProductInput(
    string Name,
    string? Description,
    double Price,
    Guid ProductTypeId,
    Guid ProductBrandId
    ) : IRequest<Product?>;
