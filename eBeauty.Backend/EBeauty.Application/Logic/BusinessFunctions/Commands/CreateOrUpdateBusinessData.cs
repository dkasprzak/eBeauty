using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Commands;

public static class CreateOrUpdateBusinessData 
{ 
    public class Request : IRequest<Result> 
    { 
        public int? Id { get; set; }
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
        public required int BusinessId { get; set; }
    }

    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext)
            : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await _currentAccountProvider.GetAuthenticatedAccount();
            
            Domain.Entities.Business? businessModel;
            Domain.Entities.Address? addressModel;
            Domain.Entities.BusinessTypeBusiness? businessTypeBusinessModel;

            if (request.Id.HasValue)
            {
                businessModel = await _applicationDbContext.Businesses
                    .Include(x => x.Account)
                    .Include(x => x.Address)
                    .Include(x => x.BusinessTypeBusiness)
                    .FirstOrDefaultAsync(x => x.Account.BusinessId == request.Id, cancellationToken);
                
                if (businessModel == null)
                {
                    throw new UnauthorizedException();
                }
                
                addressModel = businessModel.Address;
                businessTypeBusinessModel =
                    businessModel.BusinessTypeBusiness.FirstOrDefault(x => x.BusinessId == businessModel.Id);
            }
            else
            {
                addressModel = new Address();
                _applicationDbContext.Addresses.Add(addressModel);

                businessModel = new Business();
                _applicationDbContext.Businesses.Add(businessModel);

                businessTypeBusinessModel = new BusinessTypeBusiness();
                _applicationDbContext.BusinessTypeBusinesses.Add(businessTypeBusinessModel);
            }

            var businessType = await _applicationDbContext.BusinessTypes
                .FirstOrDefaultAsync(x => x.Id == request.BusinessTypeId, cancellationToken);

                            
            if (businessTypeBusinessModel == null || businessType == null)
            {
                throw new UnauthorizedException();
            }
            
            businessModel.Email = request.Email;
            businessModel.PhoneNumber = request.PhoneNumber;
            businessModel.Description = request.Description;
            businessModel.TaxNumber = request.TaxNumber;
            addressModel.Id = addressModel.Id;
            addressModel.Country = request.Country;
            addressModel.City = request.City;
            addressModel.Street = request.Street;
            addressModel.StreetNumber = request.StreetNumber;
            addressModel.PostalCode = request.PostalCode;
            businessModel.Address = addressModel;
            businessTypeBusinessModel.BusinessType = businessType;
            businessTypeBusinessModel.Business = businessModel;
            account.Business = businessModel;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Result
            {
                BusinessId = businessModel.Id
            };
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email).MaximumLength(50);
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.PhoneNumber).MaximumLength(20);

            RuleFor(x => x.TaxNumber).MaximumLength(10);

            RuleFor(x => x.Description).MaximumLength(200);

            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Country).MaximumLength(100);
            
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.City).MaximumLength(100);
            
            RuleFor(x => x.StreetNumber).NotEmpty();
            RuleFor(x => x.City).MaximumLength(50);
            
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.PostalCode).MaximumLength(10);

            RuleFor(x => x.BusinessTypeId).NotEmpty();
        }
    }
}
