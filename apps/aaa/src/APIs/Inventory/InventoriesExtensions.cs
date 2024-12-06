using Aaa.APIs.Dtos;
using Aaa.Infrastructure.Models;

namespace Aaa.APIs.Extensions;

public static class InventoriesExtensions
{
    public static Inventory ToDto(this InventoryDbModel model)
    {
        return new Inventory
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Location = model.Location,
            Quantity = model.Quantity,
        };
    }

    public static InventoryDbModel ToModel(
        this InventoryUpdateInput updateDto,
        InventoryWhereUniqueInput uniqueId
    )
    {
        var inventory = new InventoryDbModel
        {
            Id = uniqueId.Id,
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
