using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Commands;

public static class AddOpeningHoursCommand
{
    public class Request : IRequest<Result>
    {
        public List<OpeningHour> OpeningHours { get; set; } = new();
        public record OpeningHour
        {
            public DayOfWeek DayOfWeek { get; set; }
            public required string OpeningTime { get; set; }
            public required string ClosingTime { get; set; }
        }
    }
    
    public class Result
    {
        public int BusinessId { get; set; }
    }
    
    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        private readonly ICurrentBusinessProvider _businessProvider;

        public Handler(ICurrentBusinessProvider businessProvider, ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
            _businessProvider = businessProvider;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var businessId = await _businessProvider.GetBusinessId();

            if (businessId == null)
            {
                throw new UnauthorizedException();
            }
            
            var openingHours = request.OpeningHours
                .Select(oh => new Domain.Entities.OpeningHour
            {
                DayOfWeek = oh.DayOfWeek,
                OpeningTime = TimeSpan.Parse(oh.OpeningTime),
                ClosingTime = TimeSpan.Parse(oh.ClosingTime),
                BusinessId = businessId.Value
            }).ToList();
            
            _applicationDbContext.OpeningHours.AddRange(openingHours);
            await _applicationDbContext.SaveChangesAsync();

            return new Result
            {
                BusinessId = businessId.Value
            };
        }
    }
}
