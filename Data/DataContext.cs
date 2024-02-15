using EasyPay.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(e => e.Document).IsUnique();
        modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();

        modelBuilder.Entity<Transaction>().HasOne(e => e.Sender).WithMany(e => e.SentTransactions).HasForeignKey(e => e.Id);
        modelBuilder.Entity<Transaction>().HasOne(e => e.Receiver).WithMany(e => e.ReceivedTransactions).HasForeignKey(e => e.Id);
    }
}