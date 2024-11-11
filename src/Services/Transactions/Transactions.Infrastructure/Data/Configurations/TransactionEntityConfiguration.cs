using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transactions.Domain.Models;

namespace Transactions.Infrastructure.Data.Configurations;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction", t =>
        {
            t.HasCheckConstraint("CK_Transaction_Amount", "Amount > 0");
        });

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.SenderId)
            .IsRequired();

        builder.Property(e => e.SenderName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(e => e.RecipientId)
            .IsRequired();

        builder.Property(e => e.RecipientName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(u => u.Amount)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,4)");

        builder.Property(e => e.Date)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}