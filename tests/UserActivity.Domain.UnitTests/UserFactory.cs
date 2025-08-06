namespace UserActivity.Domain.UnitTests;

public static class UserFactory
{
    public static User GenerateUser(
        string? name = null, 
        string? email = null, 
        string? rawPassword = null,
        Address? address = null)
    {
        return User.Create(
            name ?? "Patrick",
            email ?? "patrick@gmail.com",
            rawPassword ?? "NotARealPassword",
            address ?? new Address("Greece", "Athena", "5th Spartans"));
    }
}
