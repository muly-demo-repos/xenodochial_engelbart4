using Microsoft.AspNetCore.Mvc;

namespace MulyDotnet.APIs;

[ApiController()]
public class LibrariesController : LibrariesControllerBase
{
    public LibrariesController(ILibrariesService service)
        : base(service) { }
}
