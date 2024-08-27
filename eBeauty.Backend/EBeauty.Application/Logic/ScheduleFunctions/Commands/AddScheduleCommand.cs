using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using MediatR;

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
}
