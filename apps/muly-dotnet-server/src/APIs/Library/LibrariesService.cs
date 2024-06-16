using MulyDotnet.Infrastructure;

namespace MulyDotnet.APIs;

public class LibrariesService : LibrariesServiceBase
{
    public LibrariesService(MulyDotnetDbContext context)
        : base(context) { }
}
