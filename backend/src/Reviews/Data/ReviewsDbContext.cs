using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Models;

namespace ShopFusion.Reviews.Data;

public class ReviewsDbContext(DbContextOptions options) 
    : DbContext(options)
{
    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<User> Users => Set<User>();
}
