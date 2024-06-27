using Aaa.Infrastructure;

namespace Aaa.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(AaaDbContext context)
        : base(context) { }
}
