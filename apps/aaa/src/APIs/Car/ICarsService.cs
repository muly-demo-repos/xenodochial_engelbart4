using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface ICarsService
{
    /// <summary>
    /// Connect multiple Sales records to Car
    /// </summary>
    public Task ConnectSales(CarIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Disconnect multiple Sales records from Car
    /// </summary>
    public Task DisconnectSales(CarIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Find multiple Sales records for Car
    /// </summary>
    public Task<List<SaleDto>> FindSales(CarIdDto idDto, SaleFindMany SaleFindMany);

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    public Task<MetadataDto> CarsMeta(CarFindMany findManyArgs);

    /// <summary>
    /// Update multiple Sales records for Car
    /// </summary>
    public Task UpdateSales(CarIdDto idDto, SaleIdDto[] salesId);

    /// <summary>
    /// Create one Car
    /// </summary>
    public Task<CarDto> CreateCar(CarCreateInput carDto);

    /// <summary>
    /// Delete one Car
    /// </summary>
    public Task DeleteCar(CarIdDto idDto);

    /// <summary>
    /// Find many Cars
    /// </summary>
    public Task<List<CarDto>> Cars(CarFindMany findManyArgs);

    /// <summary>
    /// Get one Car
    /// </summary>
    public Task<CarDto> Car(CarIdDto idDto);

    /// <summary>
    /// Update one Car
    /// </summary>
    public Task UpdateCar(CarIdDto idDto, CarUpdateInput updateDto);
}
