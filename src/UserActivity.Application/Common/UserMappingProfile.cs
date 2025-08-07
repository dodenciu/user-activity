using AutoMapper;
using UserActivity.Domain;

namespace UserActivity.Application.Common;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.TotalTransactionsAmount, opt => opt.MapFrom(src => src.GetTotalTransactionsAmount));
        CreateMap<Address, AddressResponse>();
        CreateMap<CreateAddressCommand, Address>();
    }
}
