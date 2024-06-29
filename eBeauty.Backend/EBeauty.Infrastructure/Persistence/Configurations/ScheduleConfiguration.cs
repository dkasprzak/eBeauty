using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasOne(au => au.AccountUser)
            .WithMany(s => s.Schedules)
            .HasForeignKey(fk => fk.AccountUserId);
    }
}
