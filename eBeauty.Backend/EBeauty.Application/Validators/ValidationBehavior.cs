﻿using FluentValidation;
using MediatR;
using ValidationException = EBeauty.Application.Exceptions.ValidationException;

namespace EBeauty.Application.Validators;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IList<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators.ToList();
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var errors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(v => v.Errors)
            .Where(e => e != null)
            .GroupBy(e => new { e.PropertyName, e.ErrorCode })
            .ToList();

        if (errors.Any())
        {
            throw new ValidationException()
            {
                Errors = errors.Select(e => new ValidationException.FieldError()
                {
                    Error = e.Key.ErrorCode,
                    FieldName = e.Key.PropertyName
                }).ToList(),
            };
        }

        return await next();
    }
}
