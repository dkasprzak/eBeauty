using EBeauty.Domain.Entities;
using EBeauty.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasOne(b => b.Business)
            .WithMany(s => s.Services)
            .HasForeignKey(fk => fk.BusinessId);

        builder.HasOne(bt => bt.BusinessType)
            .WithMany(s => s.Services)
            .HasForeignKey(fk => fk.BusinessTypeId);

        builder.Property(s => s.Currency)
            .HasConversion(new EnumToStringConverter<Currency>());
    }
}
