using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public static class EmployeeScheduleQuery
{
    public class Request : IRequest<Result>
    {
        public required int EmployeeAccountId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
    
    public class Result
    {
        public List<Schedule> Schedules { get; set; } = new();
        public record Schedule
        {
            public int Id { get; set; }
            public DateOnly Day { get; set; }
            public required string StartTime { get; set; }
            public required string EndTime { get; set; }
        }
    }
    
    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        private readonly ICurrentBusinessProvider _currentBusinessProvider;
        
        public Handler(ICurrentBusinessProvider currentBusinessProvider, ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
            _currentBusinessProvider = currentBusinessProvider;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var businessId = await _currentBusinessProvider.GetBusinessId();
            
            var accountUser = await _applicationDbContext.AccountUsers
                .Include(x => x.Account)
                .FirstOrDefaultAsync(a => a.Id == request.EmployeeAccountId
                                          && a.Account.BusinessId == businessId);
            
            if (accountUser is null)
            {
                throw new NotFoundException("AccountUserDoesNotExists");
            }

            var schedulesQuery = _applicationDbContext.Schedules
                .Where(x => x.AccountUserId == accountUser.Id);

            DateOnly? startDate = null;
            DateOnly? endDate = null;

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                if (DateOnly.TryParse(request.StartDate, out var parsedStartDate))
                {
                    startDate = parsedStartDate;
                }
                else
                {
                    throw new ArgumentException("InvalidStartDate");
                }
            }

            if (!string.IsNullOrEmpty(request.EndDate))
            {
                if (DateOnly.TryParse(request.EndDate, out var parsedEndDate))
                {
                    endDate = parsedEndDate;
                }
                else
                {
                    throw new ArgumentException("InvalidEndDate");
                }
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                schedulesQuery = schedulesQuery.Where(s => 
                    DateOnly.FromDateTime(s.StartTime.DateTime) >= startDate.Value &&
                    DateOnly.FromDateTime(s.StartTime.DateTime) <= endDate.Value);
            }
     
            var schedules = await schedulesQuery
                .Select(
                    r => new Result.Schedule
                    {
                        Id = r.Id,
                        Day = DateOnly.FromDateTime(r.StartTime.DateTime),
                        StartTime = TimeOnly.FromTimeSpan(r.StartTime.TimeOfDay).ToString("HH:mm"),
                        EndTime = TimeOnly.FromTimeSpan(r.EndTime.TimeOfDay).ToString("HH:mm")
                    }
                )
                .ToListAsync();

            return new Result
            {
                Schedules = schedules
            };
        }
    }
}
