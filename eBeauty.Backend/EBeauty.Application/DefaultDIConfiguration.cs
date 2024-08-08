using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Application.Services;
using EBeauty.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EBeauty.Application;

public static class DefaultDIConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();
        services.AddScoped<ICurrentBusinessProvider, CurrentBusinessProvider>();
        
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining(typeof(BaseCommandHandler));
        });
        
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(BaseQueryHandler));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
