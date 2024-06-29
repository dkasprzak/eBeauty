using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class Business : DomainEntity
{
    public required string TaxNumber { get; set; }
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Description { get; set; } = "";
    public Account Account { get; set; } = default!;
    public int AddressId { get; set; }
    public Address Address { get; set; } = default!;
    public ICollection<BusinessTypeBusiness> BusinessTypeBusiness { get; set; } = new List<BusinessTypeBusiness>();
    public List<Service> Services { get; set; } = new();
    public List<Reservation> Reservations { get; set; } = new();
    public List<OpeningHour> OpeningHours { get; set; } = new();
}
