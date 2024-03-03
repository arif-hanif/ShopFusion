using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace ShopFusion.Products.Data;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    //[Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public string? PictureFileName { get; set; }

    public string? PictureUri { get; set; }

    public int ProductTypeId { get; set; }

    public required ProductType ProductType { get; set; }

    public int CatalogBrandId { get; set; }

    public required CatalogBrand CatalogBrand { get; set; }

    // Quantity in stock
    public int AvailableStock { get; set; }

    // Available stock at which we should reorder
    public int RestockThreshold { get; set; }


    // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
    public int MaxStockThreshold { get; set; }

    /// <summary>
    /// True if item is on reorder
    /// </summary>
    public bool OnReorder { get; set; }
}
