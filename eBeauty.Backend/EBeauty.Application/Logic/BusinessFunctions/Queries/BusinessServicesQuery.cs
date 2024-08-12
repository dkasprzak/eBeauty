using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Queries;

public static class BusinessServicesQuery
{
    public class Request : IRequest<Result>
    {
        public int BusinessTypeId { get; set; }
        public int BusinessId { get; set; }
    }
    
    public class Result
    {
        public List<Service> Services { get; set; } = new();

        public record Service
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public decimal Price { get; set; }
            public Currency Currency { get; set; }
            public required string Duration { get; set; }
            public int BusinessId { get; set; }
            public int BusinessTypeId { get; set; }   
        }
    }
    
    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var services = await _applicationDbContext.Services
                .Where(x => x.BusinessTypeId == request.BusinessTypeId && x.BusinessId == request.BusinessId)
                .Select(x => new Result.Service
                {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Currency = x.Currency,
                        Duration = x.Duration.ToString(@"hh\:mm"),
                        BusinessId = x.BusinessId,
                        BusinessTypeId = x.BusinessTypeId
                }).ToListAsync();

            return new Result
            {
                Services = services
            };
        }
    }
}
