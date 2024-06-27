using Aaa.Infrastructure;

namespace Aaa.APIs;

public class InventoriesService : InventoriesServiceBase
{
    public InventoriesService(AaaDbContext context)
        : base(context) { }
}
