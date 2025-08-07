using FluentValidation;
using UserActivity.Domain;

namespace UserActivity.Application.Features.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.TransactionType)
            .NotEmpty()
            .Must(type => Enum.TryParse<TransactionType>(type, true, out _))
            .WithMessage("TransactionType must be a valid transaction type.");
    }
}

public class CreateUserTransactionCommandValidator : AbstractValidator<CreateUserTransactionCommand>
{
    public CreateUserTransactionCommandValidator()
    {
        RuleFor(x => x.TransactionCommand).SetValidator(new CreateTransactionCommandValidator());
    }
}
