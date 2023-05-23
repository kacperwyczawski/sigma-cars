namespace SigmaCars.WebApi.Features.CarType.Requests;

public record PostCarRequest(
    int DepartmentId,
    string RegistrationNumber);