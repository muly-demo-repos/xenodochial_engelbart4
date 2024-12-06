using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class EmployeesExtensions
{
    public static Employee ToDto(this EmployeeDbModel model)
    {
        return new Employee
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Position = model.Position,
            Salary = model.Salary,
            Sales = model.Sales?.Select(x => x.Id).ToList(),
        };
    }

    public static EmployeeDbModel ToModel(
        this EmployeeUpdateInput updateDto,
        EmployeeWhereUniqueInput uniqueId
    )
    {
        var employee = new EmployeeDbModel
        {
            Id = uniqueId.Id,
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
