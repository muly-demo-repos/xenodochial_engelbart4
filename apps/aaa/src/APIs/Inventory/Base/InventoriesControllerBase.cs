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
    public async Task<ActionResult<InventoryDto>> CreateInventory(InventoryCreateInput input)
    {
        var inventory = await _service.CreateInventory(input);

        return CreatedAtAction(nameof(Inventory), new { id = inventory.Id }, inventory);
    }

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteInventory([FromRoute()] InventoryIdDto idDto)
    {
        try
        {
            await _service.DeleteInventory(idDto);
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
    public async Task<ActionResult<List<InventoryDto>>> Inventories(
        [FromQuery()] InventoryFindMany filter
    )
    {
        return Ok(await _service.Inventories(filter));
    }

    /// <summary>
    /// Get one Inventory
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<InventoryDto>> Inventory([FromRoute()] InventoryIdDto idDto)
    {
        try
        {
            return await _service.Inventory(idDto);
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
        [FromQuery()] InventoryFindMany filter
    )
    {
        return Ok(await _service.InventoriesMeta(filter));
    }

    /// <summary>
    /// Update one Inventory
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateInventory(
        [FromRoute()] InventoryIdDto idDto,
        [FromQuery()] InventoryUpdateInput inventoryUpdateDto
    )
    {
        try
        {
            await _service.UpdateInventory(idDto, inventoryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
