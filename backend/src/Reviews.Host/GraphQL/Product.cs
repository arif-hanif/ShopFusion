using ShopFusion.Reviews.Domain;
using ShopFusion.Reviews.Infrastructure;

namespace ShopFusion.Reviews.Host.GraphQL;

public sealed class Product(Guid id)
{
    [ID<Product>] public Guid Id { get; } = id;

    [UsePaging(ConnectionName = "ProductReviews")]
    public IQueryable<Review> GetReviews(ReviewsDbContext context)
        => context.Reviews;
}
