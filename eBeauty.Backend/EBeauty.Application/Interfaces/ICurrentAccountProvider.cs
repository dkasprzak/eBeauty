using EBeauty.Domain.Entities;

namespace EBeauty.Application.Interfaces;

public interface ICurrentAccountProvider
{
    Task<Account> GetAuthenticatedAccount();
    Task<int?> GetAccountId();
}
