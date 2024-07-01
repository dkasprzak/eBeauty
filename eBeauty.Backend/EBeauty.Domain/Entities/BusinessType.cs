using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class BusinessType : DomainEntity
{
    public required string Name { get; set; }
    public ICollection<BusinessTypeBusiness> BusinessTypesBusinesses { get; set; } = new List<BusinessTypeBusiness>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
}
