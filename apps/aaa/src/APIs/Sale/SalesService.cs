using Aaa.Infrastructure;

namespace Aaa.APIs;

public class SalesService : SalesServiceBase
{
    public SalesService(AaaDbContext context)
        : base(context) { }
}
