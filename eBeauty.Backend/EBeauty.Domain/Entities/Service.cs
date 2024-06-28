using EBeauty.Domain.Common;
using EBeauty.Domain.Enums;

namespace EBeauty.Domain.Entities;

public class Service : DomainEntity
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public Currency Currency { get; set; }
    public TimeSpan Duration { get; set; }
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
    public int BusinessTypeId { get; set; }
    public BusinessType BusinessType { get; set; } = default!;
    public List<Reservation> Reservations = new();
}
