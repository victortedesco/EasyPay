using Cards.API.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.API.Infrastructure.Configurations;

public class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.NumberCard)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(u => u.SegureNumber)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasIndex(u => u.UserId)
            .IsUnique();

    }
}
