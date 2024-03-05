using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Data;
using ShopFusion.Reviews.Models;

namespace ShopFusion.Reviews.GraphQL;

[ExtendObjectType<Review>(IgnoreProperties = [nameof(Review.AuthorId), nameof(Review.ProductId)])]
internal static class ReviewNode
{
    public static Product GetProduct(
        [Parent] Review review)
        => new(review.ProductId);

    public static async Task<User> GetAuthorAsync(
        [Parent] Review review,
        UserByIdDataLoader userDataLoader,
        CancellationToken cancellationToken)
        => await userDataLoader.LoadAsync(review.AuthorId, cancellationToken);
    
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, Review>> GetReviewByIdAsync(
        IReadOnlyList<Guid> ids,
        IDbContextFactory<ReviewsDbContext> dbContextFactory,
        CancellationToken cancellationToken)
    {
        await using ReviewsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Reviews
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
    
    [DataLoader]
    internal static async Task<ILookup<Guid, Review>> GetReviewsByUserIdAsync(
        IReadOnlyList<Guid> ids,
        IDbContextFactory<ReviewsDbContext> dbContextFactory,
        CancellationToken cancellationToken)
    {
        await using ReviewsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var reviews = await dbContext.Users
            .Where(t => ids.Contains(t.Id))
            .SelectMany(t => t.Reviews)
            .ToListAsync(cancellationToken);

        return reviews.ToLookup(t => t.AuthorId);
    }
}
