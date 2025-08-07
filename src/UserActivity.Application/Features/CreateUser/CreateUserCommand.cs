using MediatR;
using UserActivity.Application.Common;

namespace UserActivity.Application.Features.CreateUser;

public sealed record CreateUserCommand(
    string Name, 
    string Email, 
    string RawPassword, 
    CreateAddressCommand Address) : IRequest;
