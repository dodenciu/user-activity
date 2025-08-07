using AutoMapper;
using UserActivity.Domain;

namespace UserActivity.Application.Features.ListTransactions;

public class ListTransactionsMappingProfile : Profile
{
    public ListTransactionsMappingProfile()
    {
        CreateMap<Transaction, TransactionResponse>()
            .ConstructUsing(src => new TransactionResponse(
                src.Id,
                src.Type.ToString(),
                src.Amount));
    }
}
