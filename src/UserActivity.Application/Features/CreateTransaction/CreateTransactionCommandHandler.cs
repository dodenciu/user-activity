using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Domain;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateUserTransactionCommand, Result>
{
    private readonly IAppDbContext _context;

    public CreateTransactionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(CreateUserTransactionCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        User? user = await _context.Users
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        
        TransactionType transactionType = Enum.Parse<TransactionType>(request.TransactionCommand.TransactionType, true);
        
        user.AddTransaction(transactionType, request.TransactionCommand.Amount);

        _context.Transactions.Add(user.Transactions[^1]);
        
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return Result.Success();
    }
}
