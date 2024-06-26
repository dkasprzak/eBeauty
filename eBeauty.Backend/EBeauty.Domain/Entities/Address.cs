using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class Address : DomainEntity
{
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string StreetNumber { get; set; }
    public required string PostalCode { get; set; }
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
}
