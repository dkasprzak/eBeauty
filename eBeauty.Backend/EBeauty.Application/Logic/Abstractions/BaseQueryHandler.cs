using EBeauty.Application.Interfaces;

namespace EBeauty.Application.Logic.Abstractions;

public class BaseQueryHandler
{
    protected readonly ICurrentAccountProvider _currentAccountProvider;
    protected readonly IApplicationDbContext _applicationDbContext;

    public BaseQueryHandler(ICurrentAccountProvider currentAccountProvider ,IApplicationDbContext applicationDbContext)
    {
        _currentAccountProvider = currentAccountProvider;
        _applicationDbContext = applicationDbContext;
    }
}
