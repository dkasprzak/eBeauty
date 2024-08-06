using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.UserFunctions.Commands;

public static class AssignEmployeeUserToBusinessCommand
{
    public class Request : IRequest<Result>
    {
        public required string Email { get; set; }
    }
    
    public class Result
    {
        public required int UserId { get; set; }
    }
    
    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await _currentAccountProvider.GetAuthenticatedAccount();

            var user = await _applicationDbContext.Users
                .Include(x => x.AccountUsers)
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new NotFoundException("UserDoesNotExists");
            }

            var accountExists = user.AccountUsers.Any(x => x.AccountId == account.Id);
            if (accountExists)
            {
                throw new ErrorException("AccountWithThisEmailInBusinessAlreadyExists");
            }

            var accountUser = new AccountUser
            {
                Account = account,
                User = user
            };

            _applicationDbContext.AccountUsers.Add(accountUser);
            await _applicationDbContext.SaveChangesAsync();

            return new Result
            {
                UserId = user.Id
            };
        }
    }
}
