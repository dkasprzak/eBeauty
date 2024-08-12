using EBeauty.Domain.Common;
using EBeauty.Domain.Enums;

namespace EBeauty.Domain.Entities;

public class OpeningHour : DomainEntity
{
    public DaysOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime  { get; set; }
    public TimeSpan? ClosingTime { get; set; }
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
}
