namespace BookLibraryAPIDemo.Application.Exceptions;

public sealed class UserRegistrationException(string msg) : Exception(msg);