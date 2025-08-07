namespace UserActivity.Application.Features.ListTransactions;

public sealed record TransactionResponse(Guid TransactionId, string TransactionType, decimal Amount);
