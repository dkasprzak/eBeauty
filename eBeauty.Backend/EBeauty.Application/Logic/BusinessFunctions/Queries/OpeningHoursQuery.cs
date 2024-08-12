using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Queries;

public static class OpeningHoursQuery
{
    public class Request : IRequest<Result>
    {
        public required int BusinessId { get; set; } 
    }
    
    public class Result
    {
        public List<OpeningHour> OpeningHours { get; set; } = new();
        public record OpeningHour
        {
            public int Id { get; set; }
            public DaysOfWeek DayOfWeek { get; set; }
            public string OpeningTime  { get; set; }
            public string ClosingTime { get; set; }
            public int BusinessId { get; set; }
        }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var businessId = await _applicationDbContext.Businesses
                .Where(b => b.Id == request.BusinessId)
                .Select(x => x.Id).FirstOrDefaultAsync();

            if (businessId == 0)
            {
                throw new NotFoundException("BusinessDoesNotExists");
            }
            
            var openingHours = await _applicationDbContext.OpeningHours
                .Where(x => x.BusinessId == businessId)
                .Select(x => new
                {
                    Id = x.Id,
                    DayOfWeekString = x.DayOfWeek.ToString(),
                    OpeningTime = x.OpeningTime!.Value.ToString(@"hh\:mm"),
                    ClosingTime = x.ClosingTime!.Value.ToString(@"hh\:mm"),
                    BusinessId = businessId
                })
                .ToListAsync();

            var result = openingHours
                .Select(o => new Result.OpeningHour
                {
                    Id = o.Id,
                    DayOfWeek = Enum.TryParse<DaysOfWeek>(o.DayOfWeekString, out var dayOfWeek)
                        ? dayOfWeek
                        : throw new ArgumentOutOfRangeException($"InvalidDayOfWeek"),
                    OpeningTime = o.OpeningTime,
                    ClosingTime = o.ClosingTime,
                    BusinessId = o.BusinessId
                })
                .OrderBy(o => (int)o.DayOfWeek) 
                .ToList();

            return new Result
            {
                OpeningHours = result
            };
        }
    }
}

