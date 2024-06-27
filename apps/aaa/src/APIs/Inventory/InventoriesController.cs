using Microsoft.AspNetCore.Mvc;

namespace Aaa.APIs;

[ApiController()]
public class InventoriesController : InventoriesControllerBase
{
    public InventoriesController(IInventoriesService service)
        : base(service) { }
}
