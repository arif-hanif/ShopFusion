using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Products.Models;

public class ProductBrand
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(150)]
    public required string Name { get; set; }
}
