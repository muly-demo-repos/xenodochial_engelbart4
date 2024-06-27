using Aaa.Infrastructure;

namespace Aaa.APIs;

public class CarsService : CarsServiceBase
{
    public CarsService(AaaDbContext context)
        : base(context) { }
}
