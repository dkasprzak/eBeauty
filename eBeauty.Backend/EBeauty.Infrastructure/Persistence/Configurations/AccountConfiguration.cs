using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasOne(b => b.Business)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(fk => fk.BusinessId)
            .IsRequired(false);
    }
}
