using Mediator;
using ShopFusion.Reviews.Domain;

namespace ShopFusion.Reviews.Host.GraphQL;

public record CreateReviewInput(
    string Body, 
    int Stars, 
    [property: ID<Product>] Guid ProductId, 
    [property: ID<User>] Guid AuthorId) : IRequest<Review>;
