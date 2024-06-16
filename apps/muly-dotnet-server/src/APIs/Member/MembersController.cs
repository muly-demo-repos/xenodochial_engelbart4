using Microsoft.AspNetCore.Mvc;

namespace MulyDotnet.APIs;

[ApiController()]
public class MembersController : MembersControllerBase
{
    public MembersController(IMembersService service)
        : base(service) { }
}
