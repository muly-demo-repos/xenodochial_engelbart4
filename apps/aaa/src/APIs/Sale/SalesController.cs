using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[ApiController()]
public class SalesController : SalesControllerBase
{
    public SalesController(ISalesService service)
        : base(service) { }
}
