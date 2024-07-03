namespace EBeauty.Application.Exceptions;

public class ValidationException : Exception
{
    public List<FieldError> Errors { get; set; } = new();
    
    public class FieldError
    {
        public required string FieldName { get; set; }
        public required string Error { get; set; }
    }
}
