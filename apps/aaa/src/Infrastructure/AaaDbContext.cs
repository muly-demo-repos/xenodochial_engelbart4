using Aaa.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aaa.Infrastructure;

public class AaaDbContext : IdentityDbContext<IdentityUser>
{
    public AaaDbContext(DbContextOptions<AaaDbContext> options)
        : base(options) { }

    public DbSet<Car> Cars { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Sale> Sales { get; set; }
}
