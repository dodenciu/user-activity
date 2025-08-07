namespace UserActivity.Application.Common;

public sealed record UserResponse(
    Guid Id, string Name, string Email, decimal TotalTransactionsAmount, AddressResponse Address);
public sealed record AddressResponse(
    string Country, string City, string Street);
