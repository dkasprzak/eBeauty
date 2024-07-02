using EBeauty.Application.Interfaces;

namespace EBeauty.Application.Logic.Abstractions;

public abstract class BaseCommandHandler
{
    protected readonly ICurrentAccountProvider _currentAccountProvider;
    protected readonly IApplicationDbContext _applicationDbContext;

    public BaseCommandHandler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext)
    {
        _currentAccountProvider = currentAccountProvider;
        _applicationDbContext = applicationDbContext;
    }
}
