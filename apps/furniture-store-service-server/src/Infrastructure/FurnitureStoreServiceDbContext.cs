using Microsoft.EntityFrameworkCore;

namespace FurnitureStoreService.Infrastructure;

public class FurnitureStoreServiceDbContext : DbContext
{
    public FurnitureStoreServiceDbContext(DbContextOptions<FurnitureStoreServiceDbContext> options)
        : base(options) { }
}
