using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class EmployeesExtensions
{
    public static EmployeeDto ToDto(this Employee model)
    {
        return new EmployeeDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Position = model.Position,
            Salary = model.Salary,
            Sales = model.Sales?.Select(x => new SaleIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Employee ToModel(this EmployeeUpdateInput updateDto, EmployeeIdDto idDto)
    {
        var employee = new Employee
        {
            Id = idDto.Id,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Position = updateDto.Position,
            Salary = updateDto.Salary
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            employee.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            employee.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return employee;
    }
}
