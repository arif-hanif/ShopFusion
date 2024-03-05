using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Products.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public string? Description { get; set; }
    public double Price { get; set; }
    public required Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }
    public required Guid ProductBrandId { get; set; }
    public ProductBrand? ProductBrand { get; set; }
}
