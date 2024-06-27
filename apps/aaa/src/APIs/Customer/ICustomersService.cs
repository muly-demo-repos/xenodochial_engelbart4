using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<CustomerDto> CreateCustomer(CustomerCreateInput customerDto);

    /// <summary>
    /// Connect multiple Sales records to Customer
    /// </summary>
    public Task ConnectSales(CustomerIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Disconnect multiple Sales records from Customer
    /// </summary>
    public Task DisconnectSales(CustomerIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Find multiple Sales records for Customer
    /// </summary>
    public Task<List<SaleDto>> FindSales(CustomerIdDto idDto, SaleFindMany SaleFindMany);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindMany findManyArgs);

    /// <summary>
    /// Update multiple Sales records for Customer
    /// </summary>
    public Task UpdateSales(CustomerIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerIdDto idDto);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<CustomerDto> Customer(CustomerIdDto idDto);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto);
}
