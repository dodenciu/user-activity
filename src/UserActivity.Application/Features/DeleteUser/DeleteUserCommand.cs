using MediatR;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<Result>
{
}
