using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public Handler(ICurrentBusinessProvider currentBusinessProvider, ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext)
            : base(currentAccountProvider, applicationDbContext)
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

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                if (DateTime.TryParse(request.StartDate, out var parsedStartDate))
                {
                    startDate = parsedStartDate.Date;
                }
                else
                {
                    throw new ArgumentException("InvalidStartDate");
                }
            }

            if (!string.IsNullOrEmpty(request.EndDate))
            {
                if (DateTime.TryParse(request.EndDate, out var parsedEndDate))
                {
                    endDate = parsedEndDate.Date.AddDays(1).AddTicks(-1);
                }
                else
                {
                    throw new ArgumentException("InvalidEndDate");
                }
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                schedulesQuery = schedulesQuery.Where(s =>
                    s.StartTime >= startDate.Value &&
                    s.StartTime <= endDate.Value);
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
