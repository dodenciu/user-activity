using MediatR;

namespace UserActivity.Application.Features.GetTotalTransactionAmount;

public sealed record GetTotalTransactionAmountQuery(string? TransactionType) : IRequest<decimal>;
