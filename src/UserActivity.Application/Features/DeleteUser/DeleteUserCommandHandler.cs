using MediatR;
using UserActivity.Domain;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteUserCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        User? user = await _appDbContext
            .Users
            .FindAsync([request.UserId], cancellationToken).ConfigureAwait(false);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        
        _appDbContext.Users.Remove(user);
        
        await _appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return Result.Success();
    }
}
