using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Products.Domain;

public class ProductType
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(150)]
    public required string Name { get; set; }
}
