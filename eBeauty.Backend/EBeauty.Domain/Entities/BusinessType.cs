using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class BusinessType : DomainEntity
{
    public required string Name { get; set; }
    public ICollection<BusinessType> BusinessTypes { get; set; } = new List<BusinessType>();
    public List<Service> Services { get; set; } = new();
    public List<Reservation> Reservations = new();
}
