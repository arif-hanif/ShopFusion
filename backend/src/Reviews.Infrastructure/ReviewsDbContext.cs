﻿using Microsoft.EntityFrameworkCore;
using ShopFusion.Reviews.Domain;

namespace ShopFusion.Reviews.Infrastructure;

public class ReviewsDbContext(DbContextOptions options) 
    : DbContext(options)
{
    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<User> Users => Set<User>();
}
