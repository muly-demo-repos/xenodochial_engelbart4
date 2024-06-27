using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CarsControllerBase : ControllerBase
{
    protected readonly ICarsService _service;

    public CarsControllerBase(ICarsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Connect multiple Sales records to Car
    /// </summary>
    [HttpPost("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectSales(
        [FromRoute()] CarIdDto idDto,
        [FromQuery()] SaleIdDto[] salesId
    )
    {
        try
        {
            await _service.ConnectSales(idDto, salesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Sales records from Car
    /// </summary>
    [HttpDelete("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectSales(
        [FromRoute()] CarIdDto idDto,
        [FromBody()] SaleIdDto[] salesId
    )
    {
        try
        {
            await _service.DisconnectSales(idDto, salesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Sales records for Car
    /// </summary>
    [HttpGet("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<SaleDto>>> FindSales(
        [FromRoute()] CarIdDto idDto,
        [FromQuery()] SaleFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindSales(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CarsMeta([FromQuery()] CarFindMany filter)
    {
        return Ok(await _service.CarsMeta(filter));
    }

    /// <summary>
    /// Update multiple Sales records for Car
    /// </summary>
    [HttpPatch("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSales(
        [FromRoute()] CarIdDto idDto,
        [FromBody()] SaleIdDto[] salesId
    )
    {
        try
        {
            await _service.UpdateSales(idDto, salesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Create one Car
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CarDto>> CreateCar(CarCreateInput input)
    {
        var car = await _service.CreateCar(input);

        return CreatedAtAction(nameof(Car), new { id = car.Id }, car);
    }

    /// <summary>
    /// Delete one Car
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCar([FromRoute()] CarIdDto idDto)
    {
        try
        {
            await _service.DeleteCar(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Cars
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<CarDto>>> Cars([FromQuery()] CarFindMany filter)
    {
        return Ok(await _service.Cars(filter));
    }

    /// <summary>
    /// Get one Car
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CarDto>> Car([FromRoute()] CarIdDto idDto)
    {
        try
        {
            return await _service.Car(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Car
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCar(
        [FromRoute()] CarIdDto idDto,
        [FromQuery()] CarUpdateInput carUpdateDto
    )
    {
        try
        {
            await _service.UpdateCar(idDto, carUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
