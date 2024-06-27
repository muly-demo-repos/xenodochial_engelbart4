using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class CustomersExtensions
{
    public static CustomerDto ToDto(this Customer model)
    {
        return new CustomerDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address,
            Sales = model.Sales?.Select(x => new SaleIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Customer ToModel(this CustomerUpdateInput updateDto, CustomerIdDto idDto)
    {
        var customer = new Customer
        {
            Id = idDto.Id,
            Name = updateDto.Name,
            Email = updateDto.Email,
            Phone = updateDto.Phone,
            Address = updateDto.Address
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            customer.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customer.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customer;
    }
}
