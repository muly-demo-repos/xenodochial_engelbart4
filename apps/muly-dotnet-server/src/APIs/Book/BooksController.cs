using Microsoft.AspNetCore.Mvc;

namespace MulyDotnet.APIs;

[ApiController()]
public class BooksController : BooksControllerBase
{
    public BooksController(IBooksService service)
        : base(service) { }
}
