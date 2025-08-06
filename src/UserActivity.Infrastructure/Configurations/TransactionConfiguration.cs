using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserActivity.Domain;

namespace UserActivity.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Transactions");
        builder.HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.Id)
            .HasMaxLength(36)
            .IsRequired();
        
        builder.Property(transaction => transaction.UserId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(transaction => transaction.Type)
            .IsRequired()
            .HasMaxLength(32)
            .HasConversion<string>();
        
        builder.Property(transaction => transaction.Amount)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder.Property(transaction => transaction.CreatedAt)
            .IsRequired();
        
        builder.HasIndex(transaction => transaction.UserId);
        builder.HasIndex(transaction => transaction.CreatedAt);
        builder.HasIndex(transaction => transaction.Amount);
    }

}
