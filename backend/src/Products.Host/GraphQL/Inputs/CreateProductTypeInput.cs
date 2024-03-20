using Mediator;
using ShopFusion.Products.Domain;

namespace ShopFusion.Products.GraphQL;

public sealed record CreateProductTypeInput(
    string Name
) : IRequest<ProductType?>;
