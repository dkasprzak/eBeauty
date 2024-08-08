using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Commands;

public static class EditOpeningHoursCommand
{
    public class Request : IRequest<Result>
    {
        public DayOfWeek DayOfWeek { get; set; }
        public required string OpeningTime { get; set; }
        public required string ClosingTime { get; set; }
    }
    
    public class Result
    {
        public required int BusinessId { get; set; }
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

            var openingHour = await _applicationDbContext.OpeningHours
                .FirstOrDefaultAsync(x => x.BusinessId == businessId);

            if (openingHour == null)
            {
                throw new UnauthorizedException();
            }

            openingHour.Id = openingHour.Id;
            openingHour.BusinessId = businessId.Value;
            openingHour.OpeningTime = TimeSpan.Parse(request.OpeningTime);
            openingHour.ClosingTime = TimeSpan.Parse(request.ClosingTime);
            openingHour.DayOfWeek = request.DayOfWeek;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Result
            {
                BusinessId = businessId.Value
            };

        }
    }
}
