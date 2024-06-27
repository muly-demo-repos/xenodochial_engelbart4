namespace Aaa.APIs.Dtos;

public class EmployeeUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public double? Salary { get; set; }

    public List<SaleIdDto>? Sales { get; set; }
}
