using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EmployeesControllerBase : ControllerBase
{
    protected readonly IEmployeesService _service;

    public EmployeesControllerBase(IEmployeesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Employee
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeCreateInput input)
    {
        var employee = await _service.CreateEmployee(input);

        return CreatedAtAction(nameof(Employee), new { id = employee.Id }, employee);
    }

    /// <summary>
    /// Delete one Employee
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteEmployee([FromRoute()] EmployeeIdDto idDto)
    {
        try
        {
            await _service.DeleteEmployee(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Sales records to Employee
    /// </summary>
    [HttpPost("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectSales(
        [FromRoute()] EmployeeIdDto idDto,
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
    /// Disconnect multiple Sales records from Employee
    /// </summary>
    [HttpDelete("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectSales(
        [FromRoute()] EmployeeIdDto idDto,
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
    /// Find multiple Sales records for Employee
    /// </summary>
    [HttpGet("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<SaleDto>>> FindSales(
        [FromRoute()] EmployeeIdDto idDto,
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
    /// Meta data about Employee records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EmployeesMeta(
        [FromQuery()] EmployeeFindMany filter
    )
    {
        return Ok(await _service.EmployeesMeta(filter));
    }

    /// <summary>
    /// Update multiple Sales records for Employee
    /// </summary>
    [HttpPatch("{Id}/sales")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSales(
        [FromRoute()] EmployeeIdDto idDto,
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
    /// Find many Employees
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<EmployeeDto>>> Employees(
        [FromQuery()] EmployeeFindMany filter
    )
    {
        return Ok(await _service.Employees(filter));
    }

    /// <summary>
    /// Get one Employee
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<EmployeeDto>> Employee([FromRoute()] EmployeeIdDto idDto)
    {
        try
        {
            return await _service.Employee(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Employee
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateEmployee(
        [FromRoute()] EmployeeIdDto idDto,
        [FromQuery()] EmployeeUpdateInput employeeUpdateDto
    )
    {
        try
        {
            await _service.UpdateEmployee(idDto, employeeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
