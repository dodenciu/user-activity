using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Domain;

namespace UserActivity.Application.Features.ListTransactions;

public class ListTransactionsQueryHandler : IRequestHandler<ListTransactionsQuery, IReadOnlyList<TransactionResponse>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ListTransactionsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<TransactionResponse>> Handle(ListTransactionsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        IQueryable<Transaction> query = _appDbContext.Transactions.AsQueryable();

        if (request.GreaterThanAmount.HasValue)
        {
            query = query.Where(x => x.Amount > request.GreaterThanAmount.Value);
        }
        
        IEnumerable<Transaction> transactions = await query
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        return _mapper.Map<IReadOnlyList<TransactionResponse>>(transactions);
    }
}
