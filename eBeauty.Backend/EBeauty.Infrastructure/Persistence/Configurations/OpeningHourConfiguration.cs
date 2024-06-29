using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class OpeningHourConfiguration : IEntityTypeConfiguration<OpeningHour>
{
    public void Configure(EntityTypeBuilder<OpeningHour> builder)
    {
        builder.HasOne(b => b.Business)
            .WithMany(op => op.OpeningHours)
            .HasForeignKey(fk => fk.BusinessId);
    }
}
