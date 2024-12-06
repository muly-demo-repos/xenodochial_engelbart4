using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Aaa.APIs.Extensions;
using Aaa.Infrastructure;
using Aaa.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Aaa.APIs;

public abstract class EmployeesServiceBase : IEmployeesService
{
    protected readonly AaaDbContext _context;

    public EmployeesServiceBase(AaaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Employee
    /// </summary>
    public async Task<Employee> CreateEmployee(EmployeeCreateInput createDto)
    {
        var employee = new EmployeeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Position = createDto.Position,
            Salary = createDto.Salary
        };

        if (createDto.Id != null)
        {
            employee.Id = createDto.Id;
        }
        if (createDto.Sales != null)
        {
            employee.Sales = await _context
                .Sales.Where(sale => createDto.Sales.Select(t => t.Id).Contains(sale.Id))
                .ToListAsync();
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EmployeeDbModel>(employee.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public async Task DeleteEmployee(EmployeeWhereUniqueInput uniqueId)
    {
        var employee = await _context.Employees.FindAsync(uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Sales records to Employee
    /// </summary>
    public async Task ConnectSales(
        EmployeeWhereUniqueInput uniqueId,
        SaleWhereUniqueInput[] salesId
    )
    {
        var employee = await _context
            .Employees.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
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

        var salesToConnect = sales.Except(employee.Sales);

        foreach (var sale in salesToConnect)
        {
            employee.Sales.Add(sale);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Sales records from Employee
    /// </summary>
    public async Task DisconnectSales(
        EmployeeWhereUniqueInput uniqueId,
        SaleWhereUniqueInput[] salesId
    )
    {
        var employee = await _context
            .Employees.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        var sales = await _context
            .Sales.Where(t => salesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var sale in sales)
        {
            employee.Sales?.Remove(sale);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Sales records for Employee
    /// </summary>
    public async Task<List<Sale>> FindSales(
        EmployeeWhereUniqueInput uniqueId,
        SaleFindManyArgs employeeFindManyArgs
    )
    {
        var sales = await _context
            .Sales.Where(m => m.EmployeeId == uniqueId.Id)
            .ApplyWhere(employeeFindManyArgs.Where)
            .ApplySkip(employeeFindManyArgs.Skip)
            .ApplyTake(employeeFindManyArgs.Take)
            .ApplyOrderBy(employeeFindManyArgs.SortBy)
            .ToListAsync();

        return sales.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public async Task<MetadataDto> EmployeesMeta(EmployeeFindManyArgs findManyArgs)
    {
        var count = await _context.Employees.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Sales records for Employee
    /// </summary>
    public async Task UpdateSales(EmployeeWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId)
    {
        var employee = await _context
            .Employees.Include(t => t.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
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

        employee.Sales = sales;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Employees
    /// </summary>
    public async Task<List<Employee>> Employees(EmployeeFindManyArgs findManyArgs)
    {
        var employees = await _context
            .Employees.Include(x => x.Sales)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return employees.ConvertAll(employee => employee.ToDto());
    }

    /// <summary>
    /// Get one Employee
    /// </summary>
    public async Task<Employee> Employee(EmployeeWhereUniqueInput uniqueId)
    {
        var employees = await this.Employees(
            new EmployeeFindManyArgs { Where = new EmployeeWhereInput { Id = uniqueId.Id } }
        );
        var employee = employees.FirstOrDefault();
        if (employee == null)
        {
            throw new NotFoundException();
        }

        return employee;
    }

    /// <summary>
    /// Update one Employee
    /// </summary>
    public async Task UpdateEmployee(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeUpdateInput updateDto
    )
    {
        var employee = updateDto.ToModel(uniqueId);

        if (updateDto.Sales != null)
        {
            employee.Sales = await _context
                .Sales.Where(sale => updateDto.Sales.Select(t => t).Contains(sale.Id))
                .ToListAsync();
        }

        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Employees.Any(e => e.Id == employee.Id))
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
