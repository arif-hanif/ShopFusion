using Mediator;
using ShopFusion.Reviews.Models;

namespace ShopFusion.Reviews.GraphQL;

public record CreateReviewInput(
    string Body, 
    int Stars, 
    [property: ID<Product>] Guid ProductId, 
    [property: ID<User>] Guid AuthorId) : IRequest<Review>;
