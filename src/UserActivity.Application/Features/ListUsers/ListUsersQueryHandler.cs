using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Application.Common;
using UserActivity.Domain;

namespace UserActivity.Application.Features.ListUsers;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IReadOnlyList<UserResponse>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ListUsersQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<UserResponse>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        List<User> users = await _dbContext.Users
            .AsNoTracking()
            .Include(user => user.Transactions)
            .OrderBy(user => user.Name)
            .ThenBy(user => user.Id)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return _mapper.Map<List<UserResponse>>(users);
    }
}
