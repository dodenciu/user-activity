using MediatR;
using UserActivity.Application.Common;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.GetUser;

public sealed record GetUserQuery(Guid UserId) : IRequest<Result<UserResponse>>;
