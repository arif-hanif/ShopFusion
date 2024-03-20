using Mediator;
using ShopFusion.Products.Domain;

namespace ShopFusion.Products.GraphQL;

public sealed record CreateProductBrandInput(
    string Name
) : IRequest<ProductBrand?>;
