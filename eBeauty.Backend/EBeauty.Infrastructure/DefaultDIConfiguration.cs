using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Infrastructure.Persistence;

public static class DefaultDIConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
