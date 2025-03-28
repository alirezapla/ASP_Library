namespace BookLibraryAPIDemo.Application.Exceptions;

public sealed class LogInFailedException(string msg) : Exception(msg);