using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class CarsExtensions
{
    public static Car ToDto(this CarDbModel model)
    {
        return new Car
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Make = model.Make,
            Model = model.Model,
            Year = model.Year,
            Price = model.Price,
            Sales = model.Sales?.Select(x => x.Id).ToList(),
        };
    }

    public static CarDbModel ToModel(this CarUpdateInput updateDto, CarWhereUniqueInput uniqueId)
    {
        var car = new CarDbModel
        {
            Id = uniqueId.Id,
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
