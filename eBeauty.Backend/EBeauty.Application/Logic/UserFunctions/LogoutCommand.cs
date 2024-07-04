using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using FluentValidation;
using MediatR;

namespace EBeauty.Application.Logic.UserFunctions;

public static class LogoutCommand
{
    public record Request : IRequest<Result>
    {
    }

    public record Result
    {
    }

    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) :
            base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            return new Result();
        }

        public class Validator : AbstractValidator<Request>
        {
        }
    }
}

