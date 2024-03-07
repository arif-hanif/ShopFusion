using Mediator;
using ShopFusion.Reviews.Data;
using ShopFusion.Reviews.Models;

namespace ShopFusion.Reviews.GraphQL;

public static class Operations
{
    [Query]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Review> GetReviews(ReviewsDbContext dbContext) => dbContext.Reviews;
    
    [Query]
    [NodeResolver]
    public static async Task<Review?> GetReviewById(
        Guid id,
        ReviewByIdDataLoader reviewById,
        CancellationToken cancellationToken)
        => await reviewById.LoadAsync(id, cancellationToken);
    
    [Mutation]
    public static async Task<Review?> CreateReviewAsync(
        [Service] IMediator mediator,
        CreateReviewInput input,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);
    
    [Subscription]
    [Subscribe, Topic(nameof(CreateReviewAsync))]
    public static async Task<Review> OnCreateReview(
        [EventMessage] Guid reviewId,
        ReviewByIdDataLoader reviewById,
        CancellationToken cancellationToken) 
        => await reviewById.LoadAsync(reviewId, cancellationToken);
    
    [Query]
    [NodeResolver]
    public static async Task<User?> GetUserById(
        Guid id,
        UserByIdDataLoader userById,
        CancellationToken cancellationToken)
        => await userById.LoadAsync(id, cancellationToken);
    
    [Query]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<User> GetUsers(ReviewsDbContext context) => context.Users;
    
    [Query]
    public static Product GetProductById([ID<Product>] Guid id)
        => new (id);
}
