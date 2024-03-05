using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Data;
using ShopFusion.Reviews.Models;

namespace ShopFusion.Reviews.GraphQL;

[ExtendObjectType<User>]
internal static class UserNode
{
    [UsePaging]
    public static async Task<IEnumerable<Review>> GetReviewsAsync(
        [Parent] User user,
        ReviewsByUserIdDataLoader reviewsById,
        CancellationToken cancellationToken)
        => await reviewsById.LoadAsync(user.Id, cancellationToken);

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, User>> GetUserByIdAsync(
        IReadOnlyList<Guid> ids,
        IDbContextFactory<ReviewsDbContext> dbContextFactory,
        CancellationToken cancellationToken)
    {
        await using ReviewsDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Users
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
}
