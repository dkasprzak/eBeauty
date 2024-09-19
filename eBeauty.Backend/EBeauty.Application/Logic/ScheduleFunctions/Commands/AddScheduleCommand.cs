using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.ScheduleFunctions.Commands;

public static class AddScheduleCommand
{
    public class Request : IRequest<Result>
    {
        public int AccountUserId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
    
    public class Result
    {
        public required int ScheduleId { get; set; }
    }
    
    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await _currentAccountProvider.GetAuthenticatedAccount();

            var accountUserExists = await _applicationDbContext.Accounts
                .Include(au => au.AccountUsers)
                .FirstOrDefaultAsync(a =>
                    a.BusinessId == account.BusinessId
                    && a.AccountUsers.Any(au => au.Id == request.AccountUserId));

            if (accountUserExists is null)
            {
                throw new NotFoundException("AccountUserDoesNotExists");
            }
            
            var schedule = new Schedule
            {
                AccountUserId = request.AccountUserId,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            _applicationDbContext.Schedules.Add(schedule);
            await _applicationDbContext.SaveChangesAsync();

            return new Result()
            {
                ScheduleId = schedule.Id
            };
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.AccountUserId)
                .NotEmpty();

            RuleFor(x => x.StartTime)
                .NotEmpty();

            RuleFor(x => x.EndTime)
                .NotEmpty();

            RuleFor(a => a.EndTime)
                .GreaterThan(x => x.StartTime);
;
        }
    }
}
