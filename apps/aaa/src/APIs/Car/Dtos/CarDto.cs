namespace Aaa.APIs.Dtos;

public class CarDto : CarIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public double? Price { get; set; }

    public List<SaleIdDto>? Sales { get; set; }
}
