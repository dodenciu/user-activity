using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Domain;

namespace UserActivity.Application.Features.GetTotalTransactionAmount;

public class GetTotalTransactionAmountQueryHandler : IRequestHandler<GetTotalTransactionAmountQuery, decimal>
{
    private readonly IAppDbContext _appDbContext;
    
    public GetTotalTransactionAmountQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<decimal> Handle(GetTotalTransactionAmountQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        IQueryable<Transaction> query = _appDbContext.Transactions.AsQueryable();
        
        if (!string.IsNullOrEmpty(request.TransactionType))
        {
            TransactionType transactionType = Enum.Parse<TransactionType>(request.TransactionType, true);
            query = query.Where(x => x.Type == transactionType);
        }

        decimal totalAmount = await query.SumAsync(x => x.Amount, cancellationToken).ConfigureAwait(false);

        return totalAmount;
    }
}
