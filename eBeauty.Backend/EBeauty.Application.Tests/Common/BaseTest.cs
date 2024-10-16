using EBeauty.Application.Interfaces;
using NSubstitute;

namespace EBeauty.Application.Tests.Common;

public abstract class BaseTest<THandler> where THandler : class
{
    protected readonly ICurrentBusinessProvider _businessProviderMock;
    protected readonly ICurrentAccountProvider _currentAccountProviderMock;
    protected readonly IApplicationDbContext _applicationDbContextMock;
    protected THandler _handler { get; }
    protected BaseTest(Func<ICurrentBusinessProvider?, ICurrentAccountProvider, IApplicationDbContext, THandler> handlerFactory, ICurrentBusinessProvider? businessProviderMock = null)
    {
        _businessProviderMock = businessProviderMock ?? Substitute.For<ICurrentBusinessProvider>();
        _currentAccountProviderMock = Substitute.For<ICurrentAccountProvider>();
        _applicationDbContextMock = Substitute.For<IApplicationDbContext>();
        _handler = handlerFactory(_businessProviderMock, _currentAccountProviderMock, _applicationDbContextMock);
    }
}
