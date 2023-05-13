namespace SigmaCars.WebApi.Features.Car.Queries;

public record GetCarsResponse(
    IEnumerable<GetCarResponse> Cars);

public record GetCarResponse(
    int Id,
    string DepartmentCity,
    string RegistrationNumber);