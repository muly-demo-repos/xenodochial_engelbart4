using MulyDotnet.Infrastructure;

namespace MulyDotnet.APIs;

public class AuthorsService : AuthorsServiceBase
{
    public AuthorsService(MulyDotnetDbContext context)
        : base(context) { }
}
