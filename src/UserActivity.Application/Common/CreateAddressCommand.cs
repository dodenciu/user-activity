namespace UserActivity.Application.Common;

public sealed record CreateAddressCommand(
    string Country, 
    string City, 
    string Street);
