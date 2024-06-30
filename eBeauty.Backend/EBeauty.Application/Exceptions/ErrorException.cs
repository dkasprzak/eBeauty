namespace EBeauty.Application.Exceptions;

public sealed class ErrorException : Exception
{
    public string Error { get; private set; }

    public ErrorException(string error)
    {
        Error = error;
    }
}
