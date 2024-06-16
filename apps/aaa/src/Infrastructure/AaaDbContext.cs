using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aaa.Infrastructure;

public class AaaDbContext : IdentityDbContext<IdentityUser>
{
    public AaaDbContext(DbContextOptions<AaaDbContext> options)
        : base(options) { }
}
