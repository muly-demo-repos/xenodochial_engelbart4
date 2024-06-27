using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aaa.Infrastructure.Models;

[Table("Customers")]
public class Customer
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? Phone { get; set; }

    [StringLength(1000)]
    public string? Address { get; set; }

    public List<Sale>? Sales { get; set; } = new List<Sale>();
}
