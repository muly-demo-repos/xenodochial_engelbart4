using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class InventoriesControllerBase : ControllerBase
{
    protected readonly IInventoriesService _service;

    public InventoriesControllerBase(IInventoriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Inventory
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Inventory>> CreateInventory(InventoryCreateInput input)
    {
        var inventory = await _service.CreateInventory(input);

        return CreatedAtAction(nameof(Inventory), new { id = inventory.Id }, inventory);
    }

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteInventory(
        [FromRoute()] InventoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteInventory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Inventories
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Inventory>>> Inventories(
        [FromQuery()] InventoryFindManyArgs filter
    )
    {
        return Ok(await _service.Inventories(filter));
    }

    /// <summary>
    /// Get one Inventory
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Inventory>> Inventory(
        [FromRoute()] InventoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Inventory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> InventoriesMeta(
        [FromQuery()] InventoryFindManyArgs filter
    )
    {
        return Ok(await _service.InventoriesMeta(filter));
    }

    [HttpGet("inventory-checkup")]
    [Authorize(Roles = "user")]
    public async Task<string> InventoryCheckup([FromBody()] string data)
    {
        return await _service.InventoryCheckup(data);
    }

    /// <summary>
    /// Update one Inventory
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateInventory(
        [FromRoute()] InventoryWhereUniqueInput uniqueId,
        [FromQuery()] InventoryUpdateInput inventoryUpdateDto
    )
    {
        try
        {
            await _service.UpdateInventory(uniqueId, inventoryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
