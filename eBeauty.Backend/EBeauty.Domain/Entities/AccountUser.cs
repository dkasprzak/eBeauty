using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class AccountUser : DomainEntity
{
    public int AccountId { get; set; }
    public Account Account { get; set; } = default!;
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

}
