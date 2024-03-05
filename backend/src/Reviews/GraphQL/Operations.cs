using Mediator;
using ShopFusion.Reviews.Models;

namespace ShopFusion.Reviews.GraphQL;

public static class Operations
{
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
}
