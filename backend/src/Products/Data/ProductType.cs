using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Products.Data;

public class ProductType
{
    public int Id { get; set; }

    //[Required]
    public required string Type { get; set; }
}