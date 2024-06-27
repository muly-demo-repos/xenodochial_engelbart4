using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface IInventoriesService
{
    /// <summary>
    /// Create one Inventory
    /// </summary>
    public Task<InventoryDto> CreateInventory(InventoryCreateInput inventoryDto);

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    public Task DeleteInventory(InventoryIdDto idDto);

    /// <summary>
    /// Find many Inventories
    /// </summary>
    public Task<List<InventoryDto>> Inventories(InventoryFindMany findManyArgs);

    /// <summary>
    /// Get one Inventory
    /// </summary>
    public Task<InventoryDto> Inventory(InventoryIdDto idDto);

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    public Task<MetadataDto> InventoriesMeta(InventoryFindMany findManyArgs);

    /// <summary>
    /// Update one Inventory
    /// </summary>
    public Task UpdateInventory(InventoryIdDto idDto, InventoryUpdateInput updateDto);
}
