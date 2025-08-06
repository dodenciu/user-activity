using UserActivity.Domain.Abstractions;

namespace UserActivity.Domain;

public static class TransactionErrors
{
    public static readonly UseCaseError NotFound = new(
        "Transaction.NotFound",
        "The transaction with the specified identifier was not found");
}
