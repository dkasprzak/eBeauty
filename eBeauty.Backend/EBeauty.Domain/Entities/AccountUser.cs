namespace EBeauty.Domain.Entities;

public class AccountUser
{
    public int AccountId { get; set; }
    public Account Account { get; set; } = default!;
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public List<Schedule> Schedules { get; set; } = new();
    public List<Reservation> Reservations = new();

}
