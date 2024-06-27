using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[ApiController()]
public class EmployeesController : EmployeesControllerBase
{
    public EmployeesController(IEmployeesService service)
        : base(service) { }
}
