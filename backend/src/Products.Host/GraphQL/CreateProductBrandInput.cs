using Mediator;
using ShopFusion.Products.Models;

namespace ShopFusion.Products.GraphQL;

public sealed record CreateProductBrandInput(
    string Name
) : IRequest<ProductBrand?>;
