using MediatR;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.ChangeUserPassword;

public record ChangeUserPasswordCommand(Guid UserId, ChangePasswordCommand ChangePasswordCommand) : IRequest<Result>;
public record ChangePasswordCommand(string NewPassword);
