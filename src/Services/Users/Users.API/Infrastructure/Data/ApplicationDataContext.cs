using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.API.Services.Models;

namespace Users.API.Infrastructure.Data;

public class ApplicationDataContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    public ApplicationDataContext() : base() { }

    public ApplicationDataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
