using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface ISalesService
{
    /// <summary>
    /// Create one Sale
    /// </summary>
    public Task<SaleDto> CreateSale(SaleCreateInput saleDto);

    /// <summary>
    /// Delete one Sale
    /// </summary>
    public Task DeleteSale(SaleIdDto idDto);

    /// <summary>
    /// Find many Sales
    /// </summary>
    public Task<List<SaleDto>> Sales(SaleFindMany findManyArgs);

    /// <summary>
    /// Get one Sale
    /// </summary>
    public Task<SaleDto> Sale(SaleIdDto idDto);

    /// <summary>
    /// Get a Car record for Sale
    /// </summary>
    public Task<CarDto> GetCar(SaleIdDto idDto);

    /// <summary>
    /// Get a Customer record for Sale
    /// </summary>
    public Task<CustomerDto> GetCustomer(SaleIdDto idDto);

    /// <summary>
    /// Get a Employee record for Sale
    /// </summary>
    public Task<EmployeeDto> GetEmployee(SaleIdDto idDto);

    /// <summary>
    /// Meta data about Sale records
    /// </summary>
    public Task<MetadataDto> SalesMeta(SaleFindMany findManyArgs);

    /// <summary>
    /// Update one Sale
    /// </summary>
    public Task UpdateSale(SaleIdDto idDto, SaleUpdateInput updateDto);
}
