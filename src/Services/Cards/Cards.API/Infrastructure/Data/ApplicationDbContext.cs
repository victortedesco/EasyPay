using Cards.API.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cards.API.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public required virtual DbSet<Card> Cards { get; set; }

    public ApplicationDbContext() : base() { }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cards");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
