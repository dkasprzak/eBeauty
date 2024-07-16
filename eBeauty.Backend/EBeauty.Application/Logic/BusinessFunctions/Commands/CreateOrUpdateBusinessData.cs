using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using FluentValidation;
using MediatR;

namespace EBeauty.Application.Logic.BusinessFunctions.Commands;

public static class AddBusinessData
{
    public class Request : IRequest<Result>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public required string TaxNumber { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string StreetNumber { get; set; }
        public required string PostalCode { get; set; }
        public required int BusinessTypeId { get; set; }
    }
    
    public class Result
    {
        public required int Id { get; set; }
    }
    
    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            
        }
    }
    
    public class Validator : AbstractValidator<Request>
    {
        
    }
}
