using Aaa.APIs;

namespace Aaa;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICarsService, CarsService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IInventoriesService, InventoriesService>();
        services.AddScoped<ISalesService, SalesService>();
    }
}
