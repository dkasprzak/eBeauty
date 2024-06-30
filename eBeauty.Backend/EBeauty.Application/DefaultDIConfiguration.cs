using EBeauty.Application.Logic.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Application;

public static class DefaultDIConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining(typeof(BaseCommandHandler));
        });

        return services;
    }
}
