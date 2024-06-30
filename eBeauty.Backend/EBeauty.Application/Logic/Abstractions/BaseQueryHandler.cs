using EBeauty.Application.Interfaces;

namespace EBeauty.Application.Logic.Abstractions;

public class BaseQueryHandler
{
    private readonly ICurrentAccountProvider _currentAccountProvider;
    private readonly IApplicationDbContext _applicationDbContext;

    public BaseQueryHandler(ICurrentAccountProvider currentAccountProvider ,IApplicationDbContext applicationDbContext)
    {
        _currentAccountProvider = currentAccountProvider;
        _applicationDbContext = applicationDbContext;
    }
}
