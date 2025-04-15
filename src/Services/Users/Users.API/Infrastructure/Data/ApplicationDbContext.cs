using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.API.Services.Models;

namespace Users.API.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public required virtual DbSet<User> Users { get; set; }

    public ApplicationDbContext() : base() { }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
