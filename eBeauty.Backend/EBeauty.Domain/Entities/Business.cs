using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class Business : DomainEntity
{
    public string TaxNumber { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Description { get; set; } = "";
    public Account Account { get; set; } = default!;
    public int AddressId { get; set; }
    public Address Address { get; set; } = default!;
    public ICollection<BusinessTypeBusiness> BusinessTypeBusiness { get; set; } = new List<BusinessTypeBusiness>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<OpeningHour> OpeningHours { get; set; } = new List<OpeningHour>();
}
