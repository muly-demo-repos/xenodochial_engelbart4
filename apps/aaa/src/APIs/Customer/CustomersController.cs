using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
