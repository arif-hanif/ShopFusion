using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShopFusion.Reviews.Models;

[Index(nameof(ProductId))]
public class Review
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(1024)]
    public required string Body { get; set; }
    
    public required int Stars { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    public Guid AuthorId { get; set; }
    
    [Required]
    public User? Author { get; set; }
}
