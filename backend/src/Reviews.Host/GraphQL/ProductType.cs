using ShopFusion.Reviews.Domain;
using ShopFusion.Reviews.Infrastructure;

namespace ShopFusion.Reviews.Host.GraphQL;


public class ProductType : ObjectType<Product>
{
    [ID<Product>] public Guid Id { get; }

    [UsePaging(ConnectionName = "ProductReviews")]
    public IQueryable<Review> GetReviews(ReviewsDbContext context)
        => context.Reviews;
}
