using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.UserFunctions;

public static class LoginCommand
{
    public record Request : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
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
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            
            if (user != null)
            {
                if (_passwordManager.VerifyPassword(user.HashedPassword, request.Password))
                {
                    return new Result
                    {
                        UserId = user.Id
                    };
                }
            }
            throw new ErrorException("InvalidLoginOrPassword");
        }
    }
    
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
