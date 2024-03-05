using ShopFusion.Reviews.Data;

namespace ShopFusion.Reviews.Models;

public sealed class Product(Guid id)
{
    [ID<Product>] public Guid Id { get; } = id;

    [UsePaging(ConnectionName = "ProductReviews")]
    public IQueryable<Review> GetReviews(ReviewsDbContext context)
        => context.Reviews;
}
