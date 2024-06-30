using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Application.Services;
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
        
        services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();

        return services;
    }
}
