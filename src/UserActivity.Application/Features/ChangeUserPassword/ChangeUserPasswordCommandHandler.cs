using MediatR;
using Microsoft.EntityFrameworkCore;
using UserActivity.Domain;
using UserActivity.Domain.Abstractions;

namespace UserActivity.Application.Features.ChangeUserPassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Result>
{
    private readonly IAppDbContext _appDbContext;

    public ChangeUserPasswordCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        User? user = await _appDbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        
        user.ChangePassword(request.ChangePasswordCommand.NewPassword);
        
        await _appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return Result.Success();
    }
}
