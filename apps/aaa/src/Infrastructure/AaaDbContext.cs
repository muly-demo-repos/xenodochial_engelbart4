using Aaa.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aaa.Infrastructure;

public class AaaDbContext : IdentityDbContext<IdentityUser>
{
    public AaaDbContext(DbContextOptions<AaaDbContext> options)
        : base(options) { }

    public DbSet<CarDbModel> Cars { get; set; }

    public DbSet<InventoryDbModel> Inventories { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<EmployeeDbModel> Employees { get; set; }

    public DbSet<SaleDbModel> Sales { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
