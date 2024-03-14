using HotChocolate.Subscriptions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Domain;
using ShopFusion.Reviews.Host.GraphQL;
using ShopFusion.Reviews.Infrastructure;
using ShopFusion.Shared.Errors;

namespace ShopFusion.Reviews.Host.Commands;

public class CreateReview(
    IDbContextFactory<ReviewsDbContext> dbContextFactory,
    ITopicEventSender topicEventSender
    )
    : IRequestHandler<CreateReviewInput, Review?>
{
    public async ValueTask<Review?> Handle(
        CreateReviewInput request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Body))
        {
            //throw new InvalidNameException(nameof(ProductBrand));
        }
        
        await using ReviewsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var review = new Review
        {
            Id = Guid.NewGuid(),
            Body = request.Body,
            Stars = request.Stars,
            AuthorId = request.AuthorId,
            ProductId = request.ProductId
        };

        try
        {
            await dbContext.Reviews.AddAsync(review, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await topicEventSender.SendAsync(nameof(Operations.CreateReviewAsync), review.Id, cancellationToken);
        }
        catch
        {
            throw new DatabaseOperationException();
        }

        return review;
    }
}
