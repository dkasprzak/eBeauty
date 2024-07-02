using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using EBeauty.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.UserFunctions;

public static class CreateBusinessOwnerWithAccountCommand
{
    public record Request : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string BusinessName { get; set; }
    }
    
    public record Result
    {
        public required int UserId { get; set; }
    }
    
    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        private readonly IPasswordManager _passwordManager;

        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext, IPasswordManager passwordManager) : base(currentAccountProvider, applicationDbContext)
        {
            _passwordManager = passwordManager;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var userExists = await _applicationDbContext.Users.AnyAsync(x => x.Email == request.Email);
            if (userExists)
            {
                throw new ErrorException("AccountWithThisEmailAlreadyExists");
            }

            var accountNameExists = await _applicationDbContext.Accounts.AnyAsync(x => x.Name == request.BusinessName);
            if (accountNameExists)
            {
                throw new ErrorException("AccountWithThisBusinessNameAlreadyExists");
            }

            var utcNow = DateTime.UtcNow;
            var user = new User
            {
                Email = request.Email,
                HashedPassword = "",
                RegisterDate = utcNow,
                IsActive = true,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            user.HashedPassword = _passwordManager.HashPassword(request.Password);

            _applicationDbContext.Users.Add(user);

            var account = new Account
            {
                Name = request.BusinessName,
                AccountType = AccountType.Business,
                CreateDate = utcNow
            };

            _applicationDbContext.Accounts.Add(account);

            var accountUser = new AccountUser
            {
                Account = account,
                User = user
            };

            _applicationDbContext.AccountUsers.Add(accountUser);
            
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Result
            {
                UserId = user.Id
            };
        }
    }
}
