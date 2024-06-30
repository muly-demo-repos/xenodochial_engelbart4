using Aaa.Infrastructure;

namespace Aaa.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(AaaDbContext context)
        : base(context) { }
}
