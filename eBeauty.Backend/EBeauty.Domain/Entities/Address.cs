using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class Address : DomainEntity
{
    public string Country { get; set; } = "";
    public string City { get; set; } = "";
    public string Street { get; set; } = "";
    public string StreetNumber { get; set; } = "";
    public string PostalCode { get; set; } = "";
    public Business Business { get; set; } = default!;
}
