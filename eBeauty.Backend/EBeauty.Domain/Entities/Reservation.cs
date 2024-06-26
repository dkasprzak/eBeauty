using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class Reservation : DomainEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = default!;
    public int AccountUserId { get; set; }
    public AccountUser AccountUser { get; set; } = default!;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string Comment { get; set; } = "";
}
