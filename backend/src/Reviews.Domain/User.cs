using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Reviews.Domain;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public IList<Review> Reviews { get; set; } = new List<Review>();
}
