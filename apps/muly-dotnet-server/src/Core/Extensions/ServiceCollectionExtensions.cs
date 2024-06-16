using MulyDotnet.APIs;

namespace MulyDotnet;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorsService, AuthorsService>();
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<ILibrariesService, LibrariesService>();
        services.AddScoped<IMembersService, MembersService>();
    }
}
