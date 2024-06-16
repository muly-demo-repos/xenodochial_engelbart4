using Microsoft.AspNetCore.Mvc;

namespace MulyDotnet.APIs;

[ApiController()]
public class AuthorsController : AuthorsControllerBase
{
    public AuthorsController(IAuthorsService service)
        : base(service) { }
}
