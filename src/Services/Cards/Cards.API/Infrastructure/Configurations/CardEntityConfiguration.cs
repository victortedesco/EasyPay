using Cards.API.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.API.Infrastructure.Configurations;

public class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card", t =>
        {
            t.HasCheckConstraint("CK_TotalExpenses_Amount", "TotalExpenses >= 0");
        });

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.CardNumber)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(u => u.SecurityNumber)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(u => u.TotalExpenses)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,4)");

        builder.Property(u => u.CardLimit)
            .HasDefaultValue(1000m)
            .HasColumnType("decimal(18,4)");

        builder.HasIndex(u => u.CardNumber)
            .IsUnique();
    }
}
