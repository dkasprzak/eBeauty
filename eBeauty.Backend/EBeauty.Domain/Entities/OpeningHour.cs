using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class OpeningHour : DomainEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime  { get; set; }
    public TimeSpan? ClosingTime { get; set; }
    public int BusinessId { get; set; }
    public Business Business { get; set; }
}
