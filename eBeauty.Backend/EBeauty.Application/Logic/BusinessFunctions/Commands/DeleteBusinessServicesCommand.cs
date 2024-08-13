using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Commands;

public static class DeleteBusinessServicesCommand
{
    public class Request : IRequest<Result>
    {
        public required int ServiceId { get; set; }
    }
    
    public class Result
    {
        public required int BusinessTypeId { get; set; }
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

            var service = await _applicationDbContext.Services
                .FirstOrDefaultAsync(x => x.Id == request.ServiceId && x.BusinessId == businessId);

            if (service is null)
            {
                throw new NotFoundException("BusinessServiceDoesNotExists");
            }

            _applicationDbContext.Services.Remove(service);
            await _applicationDbContext.SaveChangesAsync();

            return new Result
            {
                BusinessTypeId = service.BusinessTypeId,
                BusinessId = businessId.Value
            };
        }
    }
}
