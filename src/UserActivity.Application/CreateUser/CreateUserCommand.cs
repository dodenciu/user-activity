using MediatR;

namespace UserActivity.Application.CreateUser;

public sealed record CreateUserCommand(
    string Name, 
    string Email, 
    string RawPassword, 
    CreateAddressCommand Address) : IRequest;

public sealed record CreateAddressCommand(
    string Country, 
    string City, 
    string Street);
