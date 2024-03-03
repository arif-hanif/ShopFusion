using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Products.Data;

public class CatalogBrand
{
    public int Id { get; set; }

    //[Required]
    public required string Brand { get; set; }
}