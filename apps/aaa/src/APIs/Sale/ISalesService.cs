using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface ISalesService
{
    /// <summary>
    /// Create one Sale
    /// </summary>
    public Task<Sale> CreateSale(SaleCreateInput sale);

    /// <summary>
    /// Delete one Sale
    /// </summary>
    public Task DeleteSale(SaleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Sales
    /// </summary>
    public Task<List<Sale>> Sales(SaleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Sale
    /// </summary>
    public Task<Sale> Sale(SaleWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Car record for Sale
    /// </summary>
    public Task<Car> GetCar(SaleWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Customer record for Sale
    /// </summary>
    public Task<Customer> GetCustomer(SaleWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Employee record for Sale
    /// </summary>
    public Task<Employee> GetEmployee(SaleWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Sale records
    /// </summary>
    public Task<MetadataDto> SalesMeta(SaleFindManyArgs findManyArgs);

    /// <summary>
    /// Update one Sale
    /// </summary>
    public Task UpdateSale(SaleWhereUniqueInput uniqueId, SaleUpdateInput updateDto);
}
