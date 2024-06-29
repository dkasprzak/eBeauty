using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
{
    public void Configure(EntityTypeBuilder<AccountUser> builder)
    {
        builder.HasOne(a => a.Account)
            .WithMany(au => au.AccountUsers)
            .HasForeignKey(fk => fk.AccountId);

        builder.HasOne(u => u.User)
            .WithMany(au => au.AccountUsers)
            .HasForeignKey(fk => fk.UserId);
    }
}
