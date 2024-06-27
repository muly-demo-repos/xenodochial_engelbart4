using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface ICarsService
{
    /// <summary>
    /// Connect multiple Sales records to Car
    /// </summary>
    public Task ConnectSales(CarWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Disconnect multiple Sales records from Car
    /// </summary>
    public Task DisconnectSales(CarWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Find multiple Sales records for Car
    /// </summary>
    public Task<List<Sale>> FindSales(
        CarWhereUniqueInput uniqueId,
        SaleFindManyArgs SaleFindManyArgs
    );

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    public Task<MetadataDto> CarsMeta(CarFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Sales records for Car
    /// </summary>
    public Task UpdateSales(CarWhereUniqueInput uniqueId, SaleWhereUniqueInput[] salesId);

    /// <summary>
    /// Create one Car
    /// </summary>
    public Task<Car> CreateCar(CarCreateInput car);

    /// <summary>
    /// Delete one Car
    /// </summary>
    public Task DeleteCar(CarWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Cars
    /// </summary>
    public Task<List<Car>> Cars(CarFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Car
    /// </summary>
    public Task<Car> Car(CarWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Car
    /// </summary>
    public Task UpdateCar(CarWhereUniqueInput uniqueId, CarUpdateInput updateDto);
}
