using EBeauty.Domain.Common;
using EBeauty.Domain.Enums;

namespace EBeauty.Domain.Entities;

public class Account : DomainEntity
{
    public required string Name { get; set; }
    public required AccountType AccountType { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public int? BusinessId { get; set; }
    public Business? Business { get; set; }
    public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();
}
