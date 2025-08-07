using AutoMapper;
using UserActivity.Domain;

namespace UserActivity.Application.GetUser;

public class GetUserMappingProfile : Profile
{
    public GetUserMappingProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.TotalTransactionsAmount, opt => opt.MapFrom(src => src.GetTotalTransactionsAmount));
        CreateMap<Address, AddressResponse>();
    }
}
