using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface IEmployeesService
{
    /// <summary>
    /// Create one Employee
    /// </summary>
    public Task<EmployeeDto> CreateEmployee(EmployeeCreateInput employeeDto);

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public Task DeleteEmployee(EmployeeIdDto idDto);

    /// <summary>
    /// Connect multiple Sales records to Employee
    /// </summary>
    public Task ConnectSales(EmployeeIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Disconnect multiple Sales records from Employee
    /// </summary>
    public Task DisconnectSales(EmployeeIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Find multiple Sales records for Employee
    /// </summary>
    public Task<List<SaleDto>> FindSales(EmployeeIdDto idDto, SaleFindMany SaleFindMany);

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public Task<MetadataDto> EmployeesMeta(EmployeeFindMany findManyArgs);

    /// <summary>
    /// Update multiple Sales records for Employee
    /// </summary>
    public Task UpdateSales(EmployeeIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Find many Employees
    /// </summary>
    public Task<List<EmployeeDto>> Employees(EmployeeFindMany findManyArgs);

    /// <summary>
    /// Get one Employee
    /// </summary>
    public Task<EmployeeDto> Employee(EmployeeIdDto idDto);

    /// <summary>
    /// Update one Employee
    /// </summary>
    public Task UpdateEmployee(EmployeeIdDto idDto, EmployeeUpdateInput updateDto);
}
