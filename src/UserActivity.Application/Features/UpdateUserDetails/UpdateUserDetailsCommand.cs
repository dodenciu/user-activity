using MediatR;
using UserActivity.Application.Common;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.UpdateUserDetails;

public sealed record UpdateUserCommand(Guid UserId, UpdateUserDetailsCommand Details) : IRequest<Result>;
public sealed record UpdateUserDetailsCommand(string Name, string Email, CreateAddressCommand Address);



