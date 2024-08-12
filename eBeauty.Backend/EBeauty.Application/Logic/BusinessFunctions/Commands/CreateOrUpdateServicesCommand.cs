using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.Abstractions;
using EBeauty.Application.Validators;
using EBeauty.Domain.Entities;
using EBeauty.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBeauty.Application.Logic.BusinessFunctions.Commands;

public static class CreateOrUpdateServicesCommand
{
    public class Request : IRequest<Result>
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public required string Duration { get; set; }
        public int BusinessTypeId { get; set; }
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

            var businessTypeId = await _applicationDbContext.BusinessTypes
                .AnyAsync(bt => bt.Id == request.BusinessTypeId);

            if (!businessTypeId)
            {
                throw new NotFoundException("BusinessTypeDoesNotExists");
            }

            Service? serviceModel = null;
            if (request.Id.HasValue)
            {
                serviceModel = await _applicationDbContext.Services
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            }
            else
            {
                serviceModel = new Service
                {
                    Name = request.Name
                };
                _applicationDbContext.Services.Add(serviceModel);
            }
            
            if (serviceModel == null)
            {
                throw new UnauthorizedException();
            }

            serviceModel.Name = request.Name;
            serviceModel.Price = request.Price;
            serviceModel.Currency = request.Currency;
            serviceModel.Duration = TimeSpan.Parse(request.Duration);
            serviceModel.BusinessId = businessId.Value;
            serviceModel.BusinessTypeId = request.BusinessTypeId;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Result
            {
                BusinessId = businessId.Value
            };
        }
    }
    
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();

            RuleFor(x => x.Currency)
                .NotEmpty()
                .IsInEnum();

             RuleFor(x => x.Duration)
                 .NotEmpty()
                 .IsValidTime();
             
             RuleFor(x => x.BusinessTypeId)
                 .NotEmpty();
        }
    }
}
