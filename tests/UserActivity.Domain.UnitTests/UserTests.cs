using UserActivity.Domain.Abstractions;

namespace UserActivity.Domain.UnitTests;

public class UserTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldCreateUserWithHashedPassword()
    {
        const string rawPassword = "NotARealPassword";
        
        User user = UserFactory.GenerateUser(rawPassword: rawPassword);
        
        Assert.NotEqual(rawPassword, user.HashedPassword);
    }

    [Fact]
    public void ChangePassword_WithSamePassword_ShouldFail()
    {
        const string rawPassword = "YetAnotherRealPassword";

        User user = UserFactory.GenerateUser(rawPassword: rawPassword);

        Result result = user.ChangePassword(rawPassword);

        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.SamePassword, result.Error);
    }
    
    [Fact]
    public void ChangePassword_WithDifferentPassword_ShouldSucceed()
    {
        const string newRawPassword = "RealStrongPassword";
        string hashedPassword = BasicHasher.Hash(newRawPassword);

        User user = UserFactory.GenerateUser();

        Result result = user.ChangePassword(newRawPassword);

        Assert.True(result.IsSuccess);
        Assert.Equal(hashedPassword, user.HashedPassword);
    }
    
    [Fact]
    public void AddTransaction_WithValidParameters_ShouldIncreaseTransactionCount()
    {
        User user = UserFactory.GenerateUser();
        int initialTransactionCount = user.Transactions.Count;
        const decimal transactionAmount = 50.0m;

        user.AddTransaction(TransactionType.Credit, transactionAmount);

        Assert.Equal(initialTransactionCount + 1, user.Transactions.Count);
        Assert.Equal(transactionAmount, user.GetTotalTransactionsAmount);
    }
    
    [Fact]
    public void Update_WithValidParameters_ShouldUpdateUserProperties()
    {
        User user = UserFactory.GenerateUser();
        const string newName = "Simon";
        const string newEmail = "simon@gmail.com";
        var newAddress = new Address("Canada", "Toronto", "12th Queen Street");

        user.Update(newName, newEmail, newAddress);
        
        Assert.Equal(newName, user.Name);
        Assert.Equal(newEmail, user.Email);
        Assert.Equal(newAddress, user.Address);
    }
}
