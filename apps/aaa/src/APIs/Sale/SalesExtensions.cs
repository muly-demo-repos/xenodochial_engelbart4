using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class SalesExtensions
{
    public static Sale ToDto(this SaleDbModel model)
    {
        return new Sale
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            SaleDate = model.SaleDate,
            TotalAmount = model.TotalAmount,
            Customer = model.CustomerId,
            Car = model.CarId,
            Employee = model.EmployeeId,
        };
    }

    public static SaleDbModel ToModel(this SaleUpdateInput updateDto, SaleWhereUniqueInput uniqueId)
    {
        var sale = new SaleDbModel
        {
            Id = uniqueId.Id,
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
