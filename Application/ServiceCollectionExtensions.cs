using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        });
    }
}