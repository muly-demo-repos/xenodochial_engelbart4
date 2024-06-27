using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Aaa.APIs.Extensions;
using Aaa.Infrastructure;
using Aaa.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Aaa.APIs;

public abstract class SalesServiceBase : ISalesService
{
    protected readonly AaaDbContext _context;

    public SalesServiceBase(AaaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Sale
    /// </summary>
    public async Task<Sale> CreateSale(SaleCreateInput createDto)
    {
        var sale = new SaleDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            SaleDate = createDto.SaleDate,
            TotalAmount = createDto.TotalAmount
        };

        if (createDto.Id != null)
        {
            sale.Id = createDto.Id;
        }
        if (createDto.Car != null)
        {
            sale.Car = await _context
                .Cars.Where(car => createDto.Car.Id == car.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Customer != null)
        {
            sale.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Employee != null)
        {
            sale.Employee = await _context
                .Employees.Where(employee => createDto.Employee.Id == employee.Id)
                .FirstOrDefaultAsync();
        }

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SaleDbModel>(sale.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Sale
    /// </summary>
    public async Task DeleteSale(SaleWhereUniqueInput uniqueId)
    {
        var sale = await _context.Sales.FindAsync(uniqueId.Id);
        if (sale == null)
        {
            throw new NotFoundException();
        }

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Sales
    /// </summary>
    public async Task<List<Sale>> Sales(SaleFindManyArgs findManyArgs)
    {
        var sales = await _context
            .Sales.Include(x => x.Car)
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return sales.ConvertAll(sale => sale.ToDto());
    }

    /// <summary>
    /// Get one Sale
    /// </summary>
    public async Task<Sale> Sale(SaleWhereUniqueInput uniqueId)
    {
        var sales = await this.Sales(
            new SaleFindManyArgs { Where = new SaleWhereInput { Id = uniqueId.Id } }
        );
        var sale = sales.FirstOrDefault();
        if (sale == null)
        {
            throw new NotFoundException();
        }

        return sale;
    }

    /// <summary>
    /// Get a Car record for Sale
    /// </summary>
    public async Task<Car> GetCar(SaleWhereUniqueInput uniqueId)
    {
        var sale = await _context
            .Sales.Where(sale => sale.Id == uniqueId.Id)
            .Include(sale => sale.Car)
            .FirstOrDefaultAsync();
        if (sale == null)
        {
            throw new NotFoundException();
        }
        return sale.Car.ToDto();
    }

    /// <summary>
    /// Get a Customer record for Sale
    /// </summary>
    public async Task<Customer> GetCustomer(SaleWhereUniqueInput uniqueId)
    {
        var sale = await _context
            .Sales.Where(sale => sale.Id == uniqueId.Id)
            .Include(sale => sale.Customer)
            .FirstOrDefaultAsync();
        if (sale == null)
        {
            throw new NotFoundException();
        }
        return sale.Customer.ToDto();
    }

    /// <summary>
    /// Get a Employee record for Sale
    /// </summary>
    public async Task<Employee> GetEmployee(SaleWhereUniqueInput uniqueId)
    {
        var sale = await _context
            .Sales.Where(sale => sale.Id == uniqueId.Id)
            .Include(sale => sale.Employee)
            .FirstOrDefaultAsync();
        if (sale == null)
        {
            throw new NotFoundException();
        }
        return sale.Employee.ToDto();
    }

    /// <summary>
    /// Meta data about Sale records
    /// </summary>
    public async Task<MetadataDto> SalesMeta(SaleFindManyArgs findManyArgs)
    {
        var count = await _context.Sales.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Sale
    /// </summary>
    public async Task UpdateSale(SaleWhereUniqueInput uniqueId, SaleUpdateInput updateDto)
    {
        var sale = updateDto.ToModel(uniqueId);

        _context.Entry(sale).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Sales.Any(e => e.Id == sale.Id))
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
