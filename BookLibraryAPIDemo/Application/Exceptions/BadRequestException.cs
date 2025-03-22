namespace BookLibraryAPIDemo.Application.Exceptions;

public class BadRequestException : Exception, IAppException
{
    public BadRequestException() : base("Invalid request data.")
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
}