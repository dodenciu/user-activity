using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace UserActivity.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IAssemblyMarker).Assembly);
        
        services.AddMediatR(mediatRServiceConfiguration =>
        {
            mediatRServiceConfiguration.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly);
            mediatRServiceConfiguration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly, includeInternalTypes: true);
    }
}
