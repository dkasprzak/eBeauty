namespace EBeauty.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public string Message { get; private set; }
    public NotFoundException(string message)
    {
        Message = message;
    }
}
