using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.API.Services.Models;

namespace Users.API.Infrastructure.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(DATEDIFF(SECOND, '1970-01-01', GETUTCDATE()))");

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(u => u.Document)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(u => u.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(u => u.Balance)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(18,4)");

        builder.HasIndex(u => u.Document)
        .IsUnique();
    }
}
