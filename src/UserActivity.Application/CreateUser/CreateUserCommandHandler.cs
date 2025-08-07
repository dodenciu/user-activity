using AutoMapper;
using MediatR;
using UserActivity.Domain;
using UserActivity.Infrastructure;

namespace UserActivity.Application.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    
    public CreateUserCommandHandler(AppDbContext appDbContext, IMapper mapper)
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
        
        _appDbContext.Add(user);

        await _appDbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
