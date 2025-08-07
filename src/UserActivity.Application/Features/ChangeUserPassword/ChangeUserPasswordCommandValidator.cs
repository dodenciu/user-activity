using FluentValidation;

namespace UserActivity.Application.Features.ChangeUserPassword;

public sealed class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(command => command.ChangePasswordCommand.NewPassword)
            .NotEmpty().WithMessage("Password is required");
    }
}
