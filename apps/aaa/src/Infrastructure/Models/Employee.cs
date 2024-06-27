using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aaa.Infrastructure.Models;

[Table("Employees")]
public class Employee
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [StringLength(1000)]
    public string? Position { get; set; }

    [Range(-999999999, 999999999)]
    public double? Salary { get; set; }

    public List<Sale>? Sales { get; set; } = new List<Sale>();
}
