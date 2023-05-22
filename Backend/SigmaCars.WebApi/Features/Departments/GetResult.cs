namespace SigmaCars.WebApi.Features.Departments;

public record GetResult(
    int DepartmentId,
    string City,
    string CountryCode,
    string Address);