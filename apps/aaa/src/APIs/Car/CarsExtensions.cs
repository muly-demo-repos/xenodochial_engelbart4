using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class CarsExtensions
{
    public static CarDto ToDto(this Car model)
    {
        return new CarDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Make = model.Make,
            Model = model.Model,
            Year = model.Year,
            Price = model.Price,
            Sales = model.Sales?.Select(x => new SaleIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Car ToModel(this CarUpdateInput updateDto, CarIdDto idDto)
    {
        var car = new Car
        {
            Id = idDto.Id,
            Make = updateDto.Make,
            Model = updateDto.Model,
            Year = updateDto.Year,
            Price = updateDto.Price
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            car.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            car.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return car;
    }
}
