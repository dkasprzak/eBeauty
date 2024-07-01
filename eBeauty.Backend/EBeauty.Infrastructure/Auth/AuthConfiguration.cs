using EBeauty.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Infrastructure.Auth;

public static class AuthConfiguration
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtAuthenticationOptions>(configuration.GetSection("JwtAuthentication"));
        services.AddSingleton<JwtManager>();
        return services;
    }
    
    public static IServiceCollection AddPasswordManager(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
        services.AddScoped<IPasswordManager, PasswordManager>();
        
        return services;
    }
}
