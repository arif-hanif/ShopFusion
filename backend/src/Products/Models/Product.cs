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
    public Guid ProductTypeId { get; set; }
    public required ProductType ProductType { get; set; }
    public Guid ProductBrandId { get; set; }
    public required ProductBrand ProductBrand { get; set; }
}
