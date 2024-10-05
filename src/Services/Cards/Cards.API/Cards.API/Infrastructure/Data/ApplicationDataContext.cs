using Cards.API.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cards.API.Infrastructure.Data;

public class ApplicationDataContext : DbContext
{
    public virtual DbSet<Card> Cards { get; set; }

    public ApplicationDataContext() : base() { }

    public ApplicationDataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
