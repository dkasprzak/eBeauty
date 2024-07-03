using EBeauty.Application.Exceptions;

namespace EBeauty.WebApi.Application.Responses;

public class ValidatorErrorResponse
{
    public ValidatorErrorResponse()
    {
    }
    
    public ValidatorErrorResponse(ValidationException validationException)
    {
        if (validationException != null)
        {
            if (validationException.Errors != null)
            {
                Errors = validationException.Errors.Select(e => new FieldValidationError
                {
                    FieldName = e.FieldName,
                    Error = e.Error
                }).ToList();
            }
        }
    }

    public List<FieldValidationError> Errors { get; set; } = new();
    
    public class FieldValidationError
    {
        public required string FieldName { get; set; }
        public required string Error { get; set; }
    }
}
