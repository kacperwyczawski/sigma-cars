namespace SigmaCars.Domain.Exceptions;

public class InvalidCredentialsException : Exception
{
    public string Details { get; }

    public InvalidCredentialsException(string details) => Details = details;
}