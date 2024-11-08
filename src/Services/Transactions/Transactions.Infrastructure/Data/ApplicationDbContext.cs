using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Transactions.Domain.Models;

namespace Transactions.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public required virtual DbSet<Transaction> Transaction { get; set; }

    public ApplicationDbContext() : base() { }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("transactions");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}