﻿using EBeauty.Application.Interfaces;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Infrastructure.Persistence;

public static class SqlDatabaseConfiguration
{
    public static IServiceCollection AddSqlDatabase(this IServiceCollection services, string connectionString)
    {
        Action<IServiceProvider, DbContextOptionsBuilder> sqlOptions = (serviceProvider, options) =>
            options.UseSqlServer(connectionString,
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
                .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());

        services.AddDbContext<IApplicationDbContext, MainDbContext>(sqlOptions);

        return services;
    }
}
