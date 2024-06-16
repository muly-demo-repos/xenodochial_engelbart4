using Microsoft.EntityFrameworkCore;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.Infrastructure;

public class MulyDotnetDbContext : DbContext
{
    public MulyDotnetDbContext(DbContextOptions<MulyDotnetDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Library> Libraries { get; set; }

    public DbSet<Member> Members { get; set; }
}
