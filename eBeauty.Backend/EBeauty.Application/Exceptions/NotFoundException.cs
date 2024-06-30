namespace EBeauty.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException() : base("Not Found")
    {
    }
}
