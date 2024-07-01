using EBeauty.Infrastructure.Auth;
using EBeauty.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Infrastructure;

public static class DefaultDIConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseCache();
        services.AddSqlDatabase(configuration.GetConnectionString("MainDbSql")!);
        services.AddJwtAuth(configuration);
        return services;
    }
}
