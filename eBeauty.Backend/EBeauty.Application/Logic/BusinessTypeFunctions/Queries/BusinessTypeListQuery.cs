using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessTypeFunctions.Queries;

public static class BusinessTypeListQuery
{
    public class Request : IRequest<Result>;
    public class Result
    {
        public List<BusinessType> BusinessTypes { get; set; } = new();
        public record BusinessType
        {
            public required int Id { get; set; }
            public required string Name { get; set; }   
        }
    }
    
    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }
        
        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var businessTypes = await _applicationDbContext.BusinessTypes
                .Select(x => new Result.BusinessType
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return new Result
            {
                BusinessTypes = businessTypes
            };
        }
    }
}
