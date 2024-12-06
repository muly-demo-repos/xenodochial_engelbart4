using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Connect multiple Sales records to Customer
    /// </summary>
    public Task ConnectSales(CustomerWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Disconnect multiple Sales records from Customer
    /// </summary>
    public Task DisconnectSales(CustomerWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Find multiple Sales records for Customer
    /// </summary>
    public Task<List<Sale>> FindSales(
        CustomerWhereUniqueInput uniqueId,
        SaleFindManyArgs SaleFindManyArgs
    );

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Sales records for Customer
    /// </summary>
    public Task UpdateSales(CustomerWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);
}
