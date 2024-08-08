namespace EBeauty.Application.Interfaces;

public interface ICurrentBusinessProvider
{
    Task<int?> GetBusinessId();
}
