namespace Aaa.APIs.Dtos;

public class EmployeeCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public double? Salary { get; set; }

    public List<Sale>? Sales { get; set; }
}
