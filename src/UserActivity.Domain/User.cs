using UserActivity.Domain.Abstractions;

namespace UserActivity.Domain;

public sealed class User : Entity
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string HashedPassword { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    
    private readonly List<Transaction> _transactions = [];
    public IReadOnlyList<Transaction> Transactions => _transactions;
    
    public decimal GetTotalTransactionsAmount => _transactions.Sum(transaction => transaction.Amount);

    private User()
    {
    }
    
    private User(string name, string email, string hashedPassword, Address address) : base(Guid.NewGuid())
    {
        Name = name;
        Email = email;
        HashedPassword = hashedPassword;
        Address = address;
    }
    
    public void AddTransaction(TransactionType type, decimal amount)
    {
        var transaction = new Transaction(Id, type, amount);

        _transactions.Add(transaction);
    }
    
    public static User Create(string name, string email, string rawPassword, Address address)
    {
        string hashedPassword = BasicHasher.Hash(rawPassword);

        return new User(name, email, hashedPassword, address);
    }
    
    public Result ChangePassword(string newRawPassword)
    {
        if (HashedPassword == BasicHasher.Hash(newRawPassword))
        {
            return Result.Failure<User>(UserErrors.SamePassword);
        }
        
        HashedPassword = BasicHasher.Hash(newRawPassword);
        
        return Result.Success();
    }
    
    public void Update(string newName, string newEmail, Address newAddress)
    {
        Name = newName;
        Email = newEmail;
        Address = newAddress;
    }
}
