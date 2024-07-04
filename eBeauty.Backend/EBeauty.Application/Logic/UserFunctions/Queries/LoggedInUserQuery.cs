using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.UserFunctions.Queries;

public class LoggedInUserQuery
{
    public record Request : IRequest<Result>
    {
    }

    public record Result
    {
        public required string Email { get; set; }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        private readonly IAuthenticationDataProvider _authenticationDataProvider;

        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext, IAuthenticationDataProvider authenticationDataProvider) : base(currentAccountProvider, applicationDbContext)
        {
            _authenticationDataProvider = authenticationDataProvider;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var userId = _authenticationDataProvider.GetUserId();

            if (userId.HasValue)
            {
                var user = await _applicationDbContext.Users.Cacheable().FirstOrDefaultAsync(u => u.Id == userId.Value);
                if (user != null)
                {
                    return new Result
                    {
                        Email = user.Email
                    };
                }
            }
            throw new UnauthorizedException();
        }
    }
}
