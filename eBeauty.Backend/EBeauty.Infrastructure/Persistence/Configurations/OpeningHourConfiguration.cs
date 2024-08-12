using EBeauty.Domain.Entities;
using EBeauty.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class OpeningHourConfiguration : IEntityTypeConfiguration<OpeningHour>
{
    public void Configure(EntityTypeBuilder<OpeningHour> builder)
    {
        builder.HasOne(b => b.Business)
            .WithMany(op => op.OpeningHours)
            .HasForeignKey(fk => fk.BusinessId);

        builder.Property(o => o.DayOfWeek)
            .HasConversion(new EnumToStringConverter<DaysOfWeek>());
    }
}
