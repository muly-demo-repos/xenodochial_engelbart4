using MulyDotnet.Infrastructure;

namespace MulyDotnet.APIs;

public class BooksService : BooksServiceBase
{
    public BooksService(MulyDotnetDbContext context)
        : base(context) { }
}
