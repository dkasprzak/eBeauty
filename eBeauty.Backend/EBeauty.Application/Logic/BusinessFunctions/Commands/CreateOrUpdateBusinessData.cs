using EBeauty.Application.Exceptions;
using EBeauty.Application.Helpers;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Application.Validators;
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
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public required string TaxNumber { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        private string _streetNumber = null!;
        public required string StreetNumber
        {
            get => _streetNumber;
            set => _streetNumber = StreetNumberFormatter.FormatStreetNumber(value);
        }
        public required string PostalCode { get; set; }
        public required List<BusinessType> BusinessTypes { get; set; } = new(); 

        public record BusinessType
        {
            public required int BusinessTypeId { get; set; }
        }
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
            
            Business? businessModel;
            Address? addressModel;
            List<BusinessTypeBusiness>? businessTypeBusinessModels;

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
                businessTypeBusinessModels = businessModel.BusinessTypeBusiness.ToList();
            }
            else
            {
                addressModel = new Address();
                _applicationDbContext.Addresses.Add(addressModel);

                businessModel = new Business();
                _applicationDbContext.Businesses.Add(businessModel);

                businessTypeBusinessModels = new List<BusinessTypeBusiness>();
            }

            var businessTypes = await _applicationDbContext.BusinessTypes
                .Where(x => request.BusinessTypes.Select(bt => bt.BusinessTypeId).Contains(x.Id))
                .ToListAsync();

                            
            if (businessTypes.Count != request.BusinessTypes.Count)
            {
                throw new UnauthorizedException();
            }


            if (request.Email != null) businessModel.Email = request.Email;
            if (request.PhoneNumber != null) businessModel.PhoneNumber = request.PhoneNumber;
            if (request.Description != null) businessModel.Description = request.Description;
            businessModel.TaxNumber = request.TaxNumber;
            addressModel.Id = addressModel.Id;
            addressModel.Country = request.Country;
            addressModel.City = request.City;
            addressModel.Street = request.Street;
            addressModel.StreetNumber = request.StreetNumber.ToUpper();
            addressModel.PostalCode = request.PostalCode;
            businessModel.Address = addressModel;
            
            _applicationDbContext.BusinessTypeBusinesses.RemoveRange(businessModel.BusinessTypeBusiness);
            businessTypeBusinessModels.Clear();

            foreach (var businessType in businessTypes)
            {
                var businessTypeBusiness = new BusinessTypeBusiness
                {
                    BusinessType = businessType,
                    Business = businessModel
                };
                businessTypeBusinessModels.Add(businessTypeBusiness);
                _applicationDbContext.BusinessTypeBusinesses.Add(businessTypeBusiness);
            }

            businessModel.BusinessTypeBusiness = businessTypeBusinessModels;
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
            RuleFor(x => x.PhoneNumber)
                .PhoneNumber()
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.TaxNumber).NotEmpty();
            RuleFor(x => x.TaxNumber).MaximumLength(10);
            RuleFor(x => x.TaxNumber).PolishTaxNumber();

            RuleFor(x => x.Description).MaximumLength(200);

            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Country).MaximumLength(100);
            
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.City).MaximumLength(100);
            
            RuleFor(x => x.StreetNumber).NotEmpty();
            RuleFor(x => x.StreetNumber).MaximumLength(50);
            RuleFor(x => x.StreetNumber).StreetNumber();
            
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.PostalCode).MaximumLength(10);
            RuleFor(x => x.PostalCode).PostalCode();

            RuleFor(x => x.BusinessTypes).NotEmpty();
            RuleForEach(x => x.BusinessTypes).ChildRules(
                businessType =>
                {
                    businessType.RuleFor(x => x.BusinessTypeId).NotEmpty();
                }
            );
        }
    }
}
