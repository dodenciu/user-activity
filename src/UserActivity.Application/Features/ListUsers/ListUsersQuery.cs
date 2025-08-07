using MediatR;
using UserActivity.Application.Common;

namespace UserActivity.Application.Features.ListUsers;

public sealed record ListUsersQuery : IRequest<IReadOnlyList<UserResponse>>
{
}
