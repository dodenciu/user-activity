using AutoMapper;
using UserActivity.Domain;

namespace UserActivity.Application.Common.MappingProfiles;

public class CreateUserMappingProfile : Profile
{
    public CreateUserMappingProfile()
    {
        CreateMap<CreateAddressCommand, Address>();
    }
}
