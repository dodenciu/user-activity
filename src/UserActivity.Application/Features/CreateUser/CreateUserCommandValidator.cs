using FluentValidation;

namespace UserActivity.Application.Features.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(command => command.RawPassword)
            .NotEmpty();
        RuleFor(command => command.Address)
            .NotEmpty();
    }
}
