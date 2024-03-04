using Mediator;
using ShopFusion.Products.Models;

namespace ShopFusion.Products.GraphQL;

public sealed record CreateProductTypeInput(
    string Name
);// : IRequest<ProductType?>;
