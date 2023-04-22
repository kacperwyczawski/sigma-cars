namespace SigmaCars.Domain.Exceptions;

public class ConflictException : Exception
{
    public string Details { get; }

    public ConflictException(string details) => Details = details;
}