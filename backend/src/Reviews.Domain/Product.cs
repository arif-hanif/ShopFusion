namespace ShopFusion.Reviews.Domain;

public sealed class Product(Guid id)
{
    public Guid Id { get; } = id;
}
