using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Aaa.APIs.Extensions;
using Aaa.Infrastructure;
using Aaa.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Aaa.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly AaaDbContext _context;

    public CustomersServiceBase(AaaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name,
            Email = createDto.Email,
            Phone = createDto.Phone,
            Address = createDto.Address
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Sales != null)
        {
            customer.Sales = await _context
                .Sales.Where(sale => createDto.Sales.Select(t => t.Id).Contains(sale.Id))
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Connect multiple Sales records to Customer
    /// </summary>
    public async Task ConnectSales(
        CustomerWhereUniqueInput uniqueId,
        SaleWhereUniqueInput[] salesId
    )
    {
        var customer = await _context
            .Customers.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var sales = await _context
            .Sales.Where(t => salesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (sales.Count == 0)
        {
            throw new NotFoundException();
        }

        var salesToConnect = sales.Except(customer.Sales);

        foreach (var sale in salesToConnect)
        {
            customer.Sales.Add(sale);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Sales records from Customer
    /// </summary>
    public async Task DisconnectSales(
        CustomerWhereUniqueInput uniqueId,
        SaleWhereUniqueInput[] salesId
    )
    {
        var customer = await _context
            .Customers.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var sales = await _context
            .Sales.Where(t => salesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var sale in sales)
        {
            customer.Sales?.Remove(sale);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Sales records for Customer
    /// </summary>
    public async Task<List<Sale>> FindSales(
        CustomerWhereUniqueInput uniqueId,
        SaleFindManyArgs customerFindManyArgs
    )
    {
        var sales = await _context
            .Sales.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return sales.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Sales records for Customer
    /// </summary>
    public async Task UpdateSales(CustomerWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId)
    {
        var customer = await _context
            .Customers.Include(t => t.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var sales = await _context
            .Sales.Where(a => salesId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (sales.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Sales = sales;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Sales)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.Sales != null)
        {
            customer.Sales = await _context
                .Sales.Where(sale => updateDto.Sales.Select(t => t).Contains(sale.Id))
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
