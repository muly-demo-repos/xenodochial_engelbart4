using MulyDotnet.Infrastructure;

namespace MulyDotnet.APIs;

public class MembersService : MembersServiceBase
{
    public MembersService(MulyDotnetDbContext context)
        : base(context) { }
}
