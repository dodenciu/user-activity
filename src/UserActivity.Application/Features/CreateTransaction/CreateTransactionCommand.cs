using MediatR;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.CreateTransaction;

public sealed record CreateTransactionCommand(string TransactionType, decimal Amount) : IRequest<Result>;
public sealed record CreateUserTransactionCommand(Guid UserId, CreateTransactionCommand TransactionCommand) : IRequest<Result>;

