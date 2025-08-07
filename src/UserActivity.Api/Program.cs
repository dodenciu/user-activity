using System.Reflection;
using Microsoft.OpenApi.Models;
using UserActivity.Api.Extensions;
using UserActivity.Application;
using UserActivity.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddProblemDetails(
    options => options.CustomizeProblemDetails = context => context.HandleFluentValidation());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    var assembly = Assembly.GetExecutingAssembly();
    string? assemblyName = assembly.GetName().Name;

    swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml"));
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = assemblyName,
        Version = "v1"
    });
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.ApplyMigrations();
app.MapControllers();

await app.RunAsync().ConfigureAwait(false);

namespace UserActivity.Api
{
    /// <summary>
    /// </summary>
    public abstract partial class Program
    {
    }
}
