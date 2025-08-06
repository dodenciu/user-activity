using UserActivity.Domain.Abstractions;

namespace UserActivity.Domain;

public sealed class Transaction : Entity
{
    public Guid UserId { get; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateTimeOffset CreatedAt { get; }
    
    private Transaction()
    {
    }
    
    internal Transaction(Guid userId, TransactionType type, decimal amount) : base(Guid.NewGuid())
    {
        UserId = userId;
        Type = type;
        Amount = amount;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
