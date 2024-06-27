using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class InventoriesExtensions
{
    public static InventoryDto ToDto(this Inventory model)
    {
        return new InventoryDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Location = model.Location,
            Quantity = model.Quantity,
        };
    }

    public static Inventory ToModel(this InventoryUpdateInput updateDto, InventoryIdDto idDto)
    {
        var inventory = new Inventory
        {
            Id = idDto.Id,
            Location = updateDto.Location,
            Quantity = updateDto.Quantity
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            inventory.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            inventory.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return inventory;
    }
}
