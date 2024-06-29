using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class BusinessTypeBusinessConfiguration : IEntityTypeConfiguration<BusinessTypeBusiness>
{
    public void Configure(EntityTypeBuilder<BusinessTypeBusiness> builder)
    {
        builder.HasOne(x => x.BusinessType)
            .WithMany(bt => bt.BusinessTypesBusinesses)
            .HasForeignKey(fk => fk.BusinessTypeId);

        builder.HasOne(x => x.Business)
            .WithMany(bt => bt.BusinessTypeBusiness)
            .HasForeignKey(fk => fk.BusinessId);
    }
}
