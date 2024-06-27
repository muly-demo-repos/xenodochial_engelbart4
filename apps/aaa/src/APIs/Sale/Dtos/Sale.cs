namespace Aaa.APIs.Dtos;

public class Sale
{
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? SaleDate { get; set; }

    public double? TotalAmount { get; set; }

    public string? Customer { get; set; }

    public string? Car { get; set; }

    public string? Employee { get; set; }
}
