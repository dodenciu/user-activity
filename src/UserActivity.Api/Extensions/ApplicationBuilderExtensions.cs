using Microsoft.EntityFrameworkCore;
using UserActivity.Infrastructure;

namespace UserActivity.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}

