using EBeauty.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EBeauty.Infrastructure.Persistence;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
            var businessTypes = dbContext.BusinessTypes.ToList();
            
            if (!businessTypes.Any())
            {
                businessTypes = new List<BusinessType>
                {
                    new() { Name = "Fryzjer" },
                    new() { Name = "Barber shop" },
                    new() { Name = "Salon Kosmetyczny" },
                    new() { Name = "Paznokcie" },
                    new() { Name = "Brwi i rzęsy" },
                    new() { Name = "Masaż" },
                    new() { Name = "Zwierzaki" },
                    new() { Name = "Fizjoterapia" },
                    new() { Name = "Zdrowie" },
                    new() { Name = "Trening i Dieta" },
                    new() { Name = "Makijaż" },
                    new() { Name = "Stomatolog" },
                    new() { Name = "Podologia" },
                    new() { Name = "Medycyna Estetyczna" },
                    new() { Name = "Depilacja" },
                    new() { Name = "Tatuaż i Piercing" },
                    new() { Name = "Medycyna Naturalna" },
                    new() { Name = "Psychoterapia" },
                };
                dbContext.BusinessTypes.AddRange(businessTypes);
                dbContext.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
