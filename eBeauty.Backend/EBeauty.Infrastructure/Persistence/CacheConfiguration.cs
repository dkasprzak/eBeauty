using EFCoreSecondLevelCacheInterceptor;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Infrastructure.Persistence;

public static class CacheConfiguration
{
    public static IServiceCollection AddDatabaseCache(this IServiceCollection services)
    {
        services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(5))
                    .ConfigureLogging(true)
                    .UseCacheKeyPrefix("EF_"));
        
        return services;
    }
}
