using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aaa.Infrastructure.Models;

[Table("Sales")]
public class SaleDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public DateTime? SaleDate { get; set; }

    [Range(-999999999, 999999999)]
    public double? TotalAmount { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    public string? CarId { get; set; }

    [ForeignKey(nameof(CarId))]
    public CarDbModel? Car { get; set; } = null;

    public string? EmployeeId { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public EmployeeDbModel? Employee { get; set; } = null;
}
