using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.UserBusiness;

public static class CreateUserWithBusinessAccountCommand
{
    public class Request : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
    
    public class Result
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
        }
    }
}
