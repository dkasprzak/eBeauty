using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Queries;

public static class BusinessEmployeeUsersListQuery
{
    public record Request : IRequest<Result>;

    public record Result
    {
        public List<User> Employees { get; set; } = new();

        public record User
        {
            public required int Id { get; set; }
            public required int EmployeeAccountId { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
        }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        private readonly ICurrentBusinessProvider _currentBusinessProvider;

        public Handler(ICurrentBusinessProvider currentBusinessProvider, ICurrentAccountProvider currentAccountProvider,
            IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
            _currentBusinessProvider = currentBusinessProvider;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var businessId = await _currentBusinessProvider.GetBusinessId();

            var employees = await _applicationDbContext.Users
                .Where(u => u.AccountUsers.Any(x => x.Account.BusinessId == businessId))
                .Select(u => new Result.User
                {
                    Id = u.Id,
                    EmployeeAccountId = u.AccountUsers
                        .Where(x => x.Account.BusinessId == businessId)
                        .Select(x => x.Id)
                        .FirstOrDefault(),
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();

            return new Result
            {
                Employees = employees
            };
        }
    }
}