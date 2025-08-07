using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using UserActivity.Infrastructure;
using ApiProgram = UserActivity.Api.Program;

namespace UserActivity.IntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class IntegrationTestWebAppFactory : WebApplicationFactory<ApiProgram>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(_connection)); 
        });
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        IHost host = base.CreateHost(builder);

        using IServiceScope scope = host.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();

        return host;
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Dispose();
    }
}
