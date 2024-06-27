using Aaa.Infrastructure;

namespace Aaa.APIs;

public class EmployeesService : EmployeesServiceBase
{
    public EmployeesService(AaaDbContext context)
        : base(context) { }
}
