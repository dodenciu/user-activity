using FluentValidation;
using UserActivity.Domain;

namespace UserActivity.Application.Features.GetTotalTransactionAmount;

public class GetTotalTransactionAmountQueryValidator : AbstractValidator<GetTotalTransactionAmountQuery>
{
    public GetTotalTransactionAmountQueryValidator()
    {
        RuleFor(x => x.TransactionType)
            .Must(BeValidTransactionType)
            .When(x => !string.IsNullOrWhiteSpace(x.TransactionType))
            .WithMessage("TransactionType must be a valid value (Debit or Credit).");
    }

    private static bool BeValidTransactionType(string? transactionType)
    {
        return Enum.TryParse<TransactionType>(transactionType, true, out _);
    }
}
