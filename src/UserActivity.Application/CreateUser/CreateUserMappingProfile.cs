using AutoMapper;
using UserActivity.Domain;

namespace UserActivity.Application.CreateUser;

public class CreateUserMappingProfile : Profile
{
    public CreateUserMappingProfile()
    {
        CreateMap<CreateAddressCommand, Address>();
    }
}
