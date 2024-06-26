namespace EBeauty.Domain.Entities;

public class Schedule
{
    public int AccountUserId { get; set; }
    public AccountUser AccountUser { get; set; } = default!;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
}
 