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
    public async Task<EmployeeDto> CreateEmployee(EmployeeCreateInput createDto)
    {
        var employee = new Employee
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

        var result = await _context.FindAsync<Employee>(employee.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public async Task DeleteEmployee(EmployeeIdDto idDto)
    {
        var employee = await _context.Employees.FindAsync(idDto.Id);
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
    public async Task ConnectSales(EmployeeIdDto idDto, SaleIdDto[] salesId)
    {
        var employee = await _context
            .Employees.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
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
    public async Task DisconnectSales(EmployeeIdDto idDto, SaleIdDto[] salesId)
    {
        var employee = await _context
            .Employees.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
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
    public async Task<List<SaleDto>> FindSales(EmployeeIdDto idDto, SaleFindMany employeeFindMany)
    {
        var sales = await _context
            .Sales.Where(m => m.EmployeeId == idDto.Id)
            .ApplyWhere(employeeFindMany.Where)
            .ApplySkip(employeeFindMany.Skip)
            .ApplyTake(employeeFindMany.Take)
            .ApplyOrderBy(employeeFindMany.SortBy)
            .ToListAsync();

        return sales.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public async Task<MetadataDto> EmployeesMeta(EmployeeFindMany findManyArgs)
    {
        var count = await _context.Employees.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Sales records for Employee
    /// </summary>
    public async Task UpdateSales(EmployeeIdDto idDto, SaleIdDto[] salesId)
    {
        var employee = await _context
            .Employees.Include(t => t.Sales)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
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
    public async Task<List<EmployeeDto>> Employees(EmployeeFindMany findManyArgs)
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
    public async Task<EmployeeDto> Employee(EmployeeIdDto idDto)
    {
        var employees = await this.Employees(
            new EmployeeFindMany { Where = new EmployeeWhereInput { Id = idDto.Id } }
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
    public async Task UpdateEmployee(EmployeeIdDto idDto, EmployeeUpdateInput updateDto)
    {
        var employee = updateDto.ToModel(idDto);

        if (updateDto.Sales != null)
        {
            employee.Sales = await _context
                .Sales.Where(sale => updateDto.Sales.Select(t => t.Id).Contains(sale.Id))
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
