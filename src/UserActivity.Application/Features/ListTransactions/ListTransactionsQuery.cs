using MediatR;

namespace UserActivity.Application.Features.ListTransactions;

public record ListTransactionsQuery(decimal? GreaterThanAmount) : IRequest<IReadOnlyList<TransactionResponse>>;
