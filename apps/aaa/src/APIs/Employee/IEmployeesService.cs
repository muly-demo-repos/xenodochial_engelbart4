using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface IEmployeesService
{
    /// <summary>
    /// Create one Employee
    /// </summary>
    public Task<Employee> CreateEmployee(EmployeeCreateInput employee);

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public Task DeleteEmployee(EmployeeWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Sales records to Employee
    /// </summary>
    public Task ConnectSales(EmployeeWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Disconnect multiple Sales records from Employee
    /// </summary>
    public Task DisconnectSales(EmployeeWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Find multiple Sales records for Employee
    /// </summary>
    public Task<List<Sale>> FindSales(
        EmployeeWhereUniqueInput uniqueId,
        SaleFindManyArgs SaleFindManyArgs
    );

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public Task<MetadataDto> EmployeesMeta(EmployeeFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Sales records for Employee
    /// </summary>
    public Task UpdateSales(EmployeeWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Find many Employees
    /// </summary>
    public Task<List<Employee>> Employees(EmployeeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Employee
    /// </summary>
    public Task<Employee> Employee(EmployeeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Employee
    /// </summary>
    public Task UpdateEmployee(EmployeeWhereUniqueInput uniqueId, EmployeeUpdateInput updateDto);
}
