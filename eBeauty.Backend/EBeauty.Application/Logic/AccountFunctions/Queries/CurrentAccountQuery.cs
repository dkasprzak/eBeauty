using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.AccountFunctions.Queries;

public class CurrentAccountQuery
{
    public record Request : IRequest<Result>
    {
    }

    public record Result
    {
        public required string Name { get; set; }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {

        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await _currentAccountProvider.GetAuthenticatedAccount();

            return new Result
            {
                Name = account.Name
            };
        }
    } 
}
