using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; set; }
    DbSet<AccountUser> AccountUsers { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<Business> Businesses { get; set; }
    DbSet<BusinessType> BusinessTypes { get; set; }
    DbSet<BusinessTypeBusiness> BusinessTypeBusinesses { get; set; }
    DbSet<OpeningHour> OpeningHours { get; set; }
    DbSet<Reservation> Reservations { get; set; }
    DbSet<Schedule> Schedules { get; set; }
    DbSet<Service> Services { get; set; }
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
