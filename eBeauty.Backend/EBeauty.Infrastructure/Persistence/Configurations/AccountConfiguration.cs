using EBeauty.Domain.Entities;
using EBeauty.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EBeauty.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasOne(b => b.Business)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(fk => fk.BusinessId)
            .IsRequired(false);

        builder.Property(b => b.AccountType)
            .HasConversion(new EnumToStringConverter<AccountType>());
    }
}
