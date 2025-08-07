namespace UserActivity.Application.GetUser;

public sealed record UserResponse(
    string Name, string Email, decimal TotalTransactionsAmount, AddressResponse Address);
public sealed record AddressResponse(
    string Country, string City, string Street);
