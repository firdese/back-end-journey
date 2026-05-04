namespace TaskTracker.Application.Exceptions;

public sealed class ForbiddenResourceAccessException : Exception
{
    public ForbiddenResourceAccessException(string message) : base(message)
    {
    }
}
