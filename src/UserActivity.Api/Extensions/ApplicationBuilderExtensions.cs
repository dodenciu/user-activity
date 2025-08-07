using Microsoft.EntityFrameworkCore;
using UserActivity.Infrastructure;

namespace UserActivity.Api.Extensions;

/// <summary>
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// </summary>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        const string testingDatabaseProviderName = "Microsoft.EntityFrameworkCore.Sqlite";
        
        ArgumentNullException.ThrowIfNull(app);
        
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (dbContext.Database.IsRelational() && 
            dbContext.Database.ProviderName != testingDatabaseProviderName)
        {
            dbContext.Database.Migrate();
        }
    }
}

