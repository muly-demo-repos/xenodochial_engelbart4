namespace Aaa.APIs.Dtos;

public class SaleCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? SaleDate { get; set; }

    public double? TotalAmount { get; set; }

    public Customer? Customer { get; set; }

    public Car? Car { get; set; }

    public Employee? Employee { get; set; }
}
