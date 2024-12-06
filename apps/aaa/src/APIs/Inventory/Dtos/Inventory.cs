namespace Aaa.APIs.Dtos;

public class Inventory
{
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Location { get; set; }

    public int? Quantity { get; set; }
}
