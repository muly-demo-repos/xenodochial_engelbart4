using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[ApiController()]
public class CarsController : CarsControllerBase
{
    public CarsController(ICarsService service)
        : base(service) { }
}
