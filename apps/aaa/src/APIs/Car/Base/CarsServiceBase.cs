using Aaa.APIs;
using Aaa.APIs.Common;
using Aaa.APIs.Dtos;
using Aaa.APIs.Errors;
using Aaa.APIs.Extensions;
using Aaa.Infrastructure;
using Aaa.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Aaa.APIs;

public abstract class CarsServiceBase : ICarsService
{
    protected readonly AaaDbContext _context;

    public CarsServiceBase(AaaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Connect multiple Sales records to Car
    /// </summary>
    public async Task ConnectSales(CarWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId)
    {
        var car = await _context
            .Cars.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (car == null)
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

        var salesToConnect = sales.Except(car.Sales);

        foreach (var sale in salesToConnect)
        {
            car.Sales.Add(sale);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Sales records from Car
    /// </summary>
    public async Task DisconnectSales(CarWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId)
    {
        var car = await _context
            .Cars.Include(x => x.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (car == null)
        {
            throw new NotFoundException();
        }

        var sales = await _context
            .Sales.Where(t => salesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var sale in sales)
        {
            car.Sales?.Remove(sale);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Sales records for Car
    /// </summary>
    public async Task<List<Sale>> FindSales(
        CarWhereUniqueInput uniqueId,
        SaleFindManyArgs carFindManyArgs
    )
    {
        var sales = await _context
            .Sales.Where(m => m.CarId == uniqueId.Id)
            .ApplyWhere(carFindManyArgs.Where)
            .ApplySkip(carFindManyArgs.Skip)
            .ApplyTake(carFindManyArgs.Take)
            .ApplyOrderBy(carFindManyArgs.SortBy)
            .ToListAsync();

        return sales.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    public async Task<MetadataDto> CarsMeta(CarFindManyArgs findManyArgs)
    {
        var count = await _context.Cars.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Sales records for Car
    /// </summary>
    public async Task UpdateSales(CarWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId)
    {
        var car = await _context
            .Cars.Include(t => t.Sales)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (car == null)
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

        car.Sales = sales;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create one Car
    /// </summary>
    public async Task<Car> CreateCar(CarCreateInput createDto)
    {
        var car = new CarDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Make = createDto.Make,
            Model = createDto.Model,
            Year = createDto.Year,
            Price = createDto.Price
        };

        if (createDto.Id != null)
        {
            car.Id = createDto.Id;
        }
        if (createDto.Sales != null)
        {
            car.Sales = await _context
                .Sales.Where(sale => createDto.Sales.Select(t => t.Id).Contains(sale.Id))
                .ToListAsync();
        }

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CarDbModel>(car.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Car
    /// </summary>
    public async Task DeleteCar(CarWhereUniqueInput uniqueId)
    {
        var car = await _context.Cars.FindAsync(uniqueId.Id);
        if (car == null)
        {
            throw new NotFoundException();
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Cars
    /// </summary>
    public async Task<List<Car>> Cars(CarFindManyArgs findManyArgs)
    {
        var cars = await _context
            .Cars.Include(x => x.Sales)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return cars.ConvertAll(car => car.ToDto());
    }

    /// <summary>
    /// Get one Car
    /// </summary>
    public async Task<Car> Car(CarWhereUniqueInput uniqueId)
    {
        var cars = await this.Cars(
            new CarFindManyArgs { Where = new CarWhereInput { Id = uniqueId.Id } }
        );
        var car = cars.FirstOrDefault();
        if (car == null)
        {
            throw new NotFoundException();
        }

        return car;
    }

    /// <summary>
    /// Update one Car
    /// </summary>
    public async Task UpdateCar(CarWhereUniqueInput uniqueId, CarUpdateInput updateDto)
    {
        var car = updateDto.ToModel(uniqueId);

        if (updateDto.Sales != null)
        {
            car.Sales = await _context
                .Sales.Where(sale => updateDto.Sales.Select(t => t).Contains(sale.Id))
                .ToListAsync();
        }

        _context.Entry(car).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Cars.Any(e => e.Id == car.Id))
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
