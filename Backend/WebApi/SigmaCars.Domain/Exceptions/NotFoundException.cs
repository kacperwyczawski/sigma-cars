namespace SigmaCars.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string details) => Details = details;

    public NotFoundException(string entity, string action)
        : this($"{entity} you are attempting to {action} could not be found.")
    {
    }

    public string Details { get; }
}