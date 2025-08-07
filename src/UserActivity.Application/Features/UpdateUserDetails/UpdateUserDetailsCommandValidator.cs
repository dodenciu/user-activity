using FluentValidation;

namespace UserActivity.Application.Features.UpdateUserDetails;

public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(command => command.UserId).NotEmpty();
        RuleFor(command => command.Details.Name).NotEmpty();
        RuleFor(command => command.Details.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(command => command.Details.Address)
            .NotEmpty();
    }
}
