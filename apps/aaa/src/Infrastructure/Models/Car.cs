using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aaa.Infrastructure.Models;

[Table("Cars")]
public class Car
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Make { get; set; }

    [StringLength(1000)]
    public string? Model { get; set; }

    [Range(-999999999, 999999999)]
    public int? Year { get; set; }

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    public List<Sale>? Sales { get; set; } = new List<Sale>();
}
