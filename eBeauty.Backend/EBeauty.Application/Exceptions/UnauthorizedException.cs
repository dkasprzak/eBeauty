namespace EBeauty.Application.Exceptions;

public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Unauthorized")
    {
    }
}
