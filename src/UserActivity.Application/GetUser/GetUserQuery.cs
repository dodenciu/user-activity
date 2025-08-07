using MediatR;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.GetUser;

public sealed record GetUserQuery(Guid UserId) : IRequest<Result<UserResponse>>;
