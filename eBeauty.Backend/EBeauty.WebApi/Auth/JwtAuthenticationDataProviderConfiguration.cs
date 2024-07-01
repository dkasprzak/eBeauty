using EBeauty.Application.Interfaces;

namespace EBeauty.WebApi.Auth;

public static class JwtAuthenticationDataProviderConfiguration
{
    public static IServiceCollection AddJwtAuthenticationDataProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CookieSettings>(configuration.GetSection("CookieSettings"));
        services.AddScoped<IAuthenticationDataProvider, JwtAuthenticationDataProvider>();
        return services;
    }
}
