namespace EBeauty.Domain.Entities;

public class BusinessTypeBussines
{
    public required int BusinessTypeId { get; set; }
    public BusinessType BusinessType { get; set; } = default!;
    public int BusinessId { get; set; }
    public Business Business { get; set; } = default!;
}
