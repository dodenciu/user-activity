using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Application.Common;
using UserActivity.Domain;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserResponse>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        User? user = await _appDbContext
            .Users
            .AsNoTracking()
            .Include(user => user.Transactions)
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken)
            .ConfigureAwait(false);
        
        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound);
        }

        UserResponse userResponse = _mapper.Map<UserResponse>(user);

        return Result.Success(userResponse);
    }
}
