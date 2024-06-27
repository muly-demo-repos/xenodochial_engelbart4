namespace Aaa.APIs.Dtos;

public class SaleUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? SaleDate { get; set; }

    public double? TotalAmount { get; set; }

    public CustomerIdDto? Customer { get; set; }

    public CarIdDto? Car { get; set; }

    public EmployeeIdDto? Employee { get; set; }
}
