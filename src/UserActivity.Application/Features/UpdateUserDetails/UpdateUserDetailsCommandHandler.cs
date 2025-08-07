using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Domain;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateUserDetailsCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        User? user = await _appDbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        
        Address address = _mapper.Map<Address>(request.Details.Address);
        
        user.Update(request.Details.Name, request.Details.Email, address);

        await _appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return Result.Success();
    }
}
