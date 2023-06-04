namespace SigmaCars.WebApi.Features.CarType.RequestsAndResponses;

public record PostCarRequest(
    int DepartmentId,
    string RegistrationNumber);