using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using EBeauty.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.UserFunctions.Commands;

public static class CreateCustomerUserWithAccountCommand
{
    public record Request : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
    
    public record Result
    {
        public required int UserId { get; set; }
    }

    public class Handler :  BaseCommandHandler, IRequestHandler<Request, Result>
    {
        private readonly IPasswordManager _passwordManager;

        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext, IPasswordManager passwordManager) : base(currentAccountProvider, applicationDbContext)
        {
            _passwordManager = passwordManager;
        }
        
        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var accountExists = await _applicationDbContext.Users.AnyAsync(x => x.Email == request.Email);
            if (accountExists)
            {
                throw new ErrorException("AccountWithThisEmailAlreadyExists");
            }

            var utcNow = DateTime.UtcNow;
            
            var user = new User
            {
                Email = request.Email,
                HashedPassword = "",
                FirstName = request.FirstName,
                LastName = request.LastName,
                RegisterDate = utcNow,
                IsActive = true
            };

            user.HashedPassword = _passwordManager.HashPassword(request.Password);
            _applicationDbContext.Users.Add(user);

            var account = new Account
            {
                Name = request.Email,
                AccountType = AccountType.Customer,
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
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Email).EmailAddress();
                RuleFor(x => x.Email).MaximumLength(100);
                
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Password).MaximumLength(200);
                
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.FirstName).MaximumLength(100);
                
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.LastName).MaximumLength(100);
            }
        }
    }
}
