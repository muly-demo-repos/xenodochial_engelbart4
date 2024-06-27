using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class SalesExtensions
{
    public static SaleDto ToDto(this Sale model)
    {
        return new SaleDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            SaleDate = model.SaleDate,
            TotalAmount = model.TotalAmount,
            Customer = new CustomerIdDto { Id = model.CustomerId },
            Car = new CarIdDto { Id = model.CarId },
            Employee = new EmployeeIdDto { Id = model.EmployeeId },
        };
    }

    public static Sale ToModel(this SaleUpdateInput updateDto, SaleIdDto idDto)
    {
        var sale = new Sale
        {
            Id = idDto.Id,
            SaleDate = updateDto.SaleDate,
            TotalAmount = updateDto.TotalAmount
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            sale.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            sale.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return sale;
    }
}
