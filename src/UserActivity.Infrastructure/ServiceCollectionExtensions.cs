using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UserActivity.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddOptions<UserActivityPostgreSqlOptions>()
            .Bind(configuration.GetSection(nameof(UserActivityPostgreSqlOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>((sp, options) =>
            options.UseNpgsql(
                    sp.GetRequiredService<IOptions<UserActivityPostgreSqlOptions>>()
                        .Value.GetConnectionString())
                .UseSnakeCaseNamingConvention());
    }
}
