using EBeauty.Application.Interfaces;
using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Infrastructure.Persistence;

public class MainDbContext : DbContext, IApplicationDbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountUser> AccountUsers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<BusinessType> BusinessTypes { get; set; }
    public DbSet<BusinessTypeBusiness> BusinessTypeBusinesses { get; set; }
    public DbSet<OpeningHour> OpeningHours { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 4);
        base.ConfigureConventions(configurationBuilder);
    }
}
