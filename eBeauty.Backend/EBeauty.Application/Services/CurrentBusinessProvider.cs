using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Services;

public class CurrentBusinessProvider : ICurrentBusinessProvider
{
    private readonly ICurrentAccountProvider _currentAccountProvider;
    private readonly IApplicationDbContext _applicationDbContext;

    public CurrentBusinessProvider(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext)
    {
        _currentAccountProvider = currentAccountProvider;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<int?> GetBusinessId()
    {
        var accountId = await _currentAccountProvider.GetAccountId();

        if (accountId == null)
        {
            throw new UnauthorizedException();
        }
        
        var businessId = await _applicationDbContext
            .Businesses
            .Include(x => x.Account)
            .Where(x => x.Account.Id == accountId)
            .Select(x => x.Id)
            .Cacheable()
            .FirstOrDefaultAsync();

        if (businessId != 0)
        {
            return businessId;
        }
        return null;
    }
}
