using Microsoft.EntityFrameworkCore;
using UserActivity.Domain;

namespace UserActivity.Application;

public interface IAppDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Transaction> Transactions { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
