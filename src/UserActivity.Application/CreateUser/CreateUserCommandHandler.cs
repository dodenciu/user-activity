using AutoMapper;
using MediatR;
using UserActivity.Domain;

namespace UserActivity.Application.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    
    public CreateUserCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(appDbContext);
        
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Address address = _mapper.Map<Address>(request.Address);
        
        var user = User.Create(
            request.Name, 
            request.Email, 
            request.RawPassword, 
            address);
        
        _appDbContext.Users.Add(user);

        await _appDbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
