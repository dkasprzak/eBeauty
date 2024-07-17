using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Queries;

public static class BusinessDetailQuery
{
    public class Request : IRequest<Result>
    {
        public required int Id { get; set; }
    }
    
    public class Result
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public required string TaxNumber { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string StreetNumber { get; set; }
        public required string PostalCode { get; set; }
        public required List<BusinessType> BusinessTypes { get; set; } = new();
        public record BusinessType
        {
            public required int BusinessTypeId { get; set; }
            public required string BusinessTypeName { get; set; }   
        }
    }
    
    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await _currentAccountProvider.GetAuthenticatedAccount();
            var model = await _applicationDbContext.Businesses
                .Include(x => x.Account)
                .Include(x => x.Address)
                .Include(x => x.BusinessTypeBusiness)
                .ThenInclude(btb => btb.BusinessType) 
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.Account.Id == account.Id);

            if (model == null)
            {
                throw new UnauthorizedException();
            }
            
            
            return new Result
            {
                Id = model.Id,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Description = model.Description,
                TaxNumber = model.TaxNumber,
                Country = model.Address.Country,
                City = model.Address.City,
                Street = model.Address.Street,
                StreetNumber = model.Address.StreetNumber,
                PostalCode = model.Address.PostalCode,
                BusinessTypes = model.BusinessTypeBusiness
                    .Select(btb => new Result.BusinessType
                    {
                        BusinessTypeId = btb.BusinessType.Id,
                        BusinessTypeName = btb.BusinessType.Name
                    }).ToList()
            };

        }
    }
}
