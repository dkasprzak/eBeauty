using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasOne(u => u.User)
            .WithMany(r => r.Reservations)
            .HasForeignKey(fk => fk.UserId);
        
        builder.HasOne(b => b.Business)
            .WithMany(r => r.Reservations)
            .HasForeignKey(fk => fk.BusinessId);

        builder.HasOne(s => s.Service)
            .WithMany(r => r.Reservations)
            .HasForeignKey(fk => fk.ServiceId);

        builder.HasOne(au => au.AccountUser)
            .WithMany(r => r.Reservations)
            .HasForeignKey(fk => fk.AccountUserId);
    }
}
