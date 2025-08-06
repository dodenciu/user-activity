using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserActivity.Domain;

namespace UserActivity.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(user => user.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(user => user.Email)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(user => user.HashedPassword)
            .HasMaxLength(44)
            .IsRequired();
        
        builder.ComplexProperty(user => user.Address, addressBuilder =>
        {
            addressBuilder.Property(address => address.Country)
                .HasColumnName(nameof(Address.Country))
                .HasMaxLength(100);
            addressBuilder.Property(address => address.City)
                .HasColumnName(nameof(Address.City))
                .HasMaxLength(100);
            addressBuilder.Property(address => address.Street)
                .HasColumnName(nameof(Address.Street))
                .HasMaxLength(100);
        });

        builder.HasMany(user => user.Transactions)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey(transaction => transaction.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
