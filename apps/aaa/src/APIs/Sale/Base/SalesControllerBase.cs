using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SalesControllerBase : ControllerBase
{
    protected readonly ISalesService _service;

    public SalesControllerBase(ISalesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Sale
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Sale>> CreateSale(SaleCreateInput input)
    {
        var sale = await _service.CreateSale(input);

        return CreatedAtAction(nameof(Sale), new { id = sale.Id }, sale);
    }

    /// <summary>
    /// Delete one Sale
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteSale([FromRoute()] SaleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSale(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Sales
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Sale>>> Sales([FromQuery()] SaleFindManyArgs filter)
    {
        return Ok(await _service.Sales(filter));
    }

    /// <summary>
    /// Get one Sale
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Sale>> Sale([FromRoute()] SaleWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Sale(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Car record for Sale
    /// </summary>
    [HttpGet("{Id}/cars")]
    public async Task<ActionResult<List<Car>>> GetCar([FromRoute()] SaleWhereUniqueInput uniqueId)
    {
        var car = await _service.GetCar(uniqueId);
        return Ok(car);
    }

    /// <summary>
    /// Get a Customer record for Sale
    /// </summary>
    [HttpGet("{Id}/customers")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] SaleWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Get a Employee record for Sale
    /// </summary>
    [HttpGet("{Id}/employees")]
    public async Task<ActionResult<List<Employee>>> GetEmployee(
        [FromRoute()] SaleWhereUniqueInput uniqueId
    )
    {
        var employee = await _service.GetEmployee(uniqueId);
        return Ok(employee);
    }

    /// <summary>
    /// Meta data about Sale records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SalesMeta([FromQuery()] SaleFindManyArgs filter)
    {
        return Ok(await _service.SalesMeta(filter));
    }

    /// <summary>
    /// Update one Sale
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSale(
        [FromRoute()] SaleWhereUniqueInput uniqueId,
        [FromQuery()] SaleUpdateInput saleUpdateDto
    )
    {
        try
        {
            await _service.UpdateSale(uniqueId, saleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
