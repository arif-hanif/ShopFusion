using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Basket.Host.Data;

public class BasketItem : IValidatableObject
{
    public required string Id { get; set; }
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? PictureUrl { get; set; }

    private static readonly string[] memberNames = new[] { "Quantity" };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (Quantity < 1)
        {
            results.Add(new ValidationResult("Invalid number of units", memberNames));
        }

        return results;
    }
}
