namespace SigmaCars.WebApi.Features.Rental.Queries;

public record GetRentalsResponse(
    IEnumerable<GetRentalResponse> Rentals);

public record GetRentalResponse(
    string CarMake,
    string CarType,
    string CarRegistrationNumber,
    string DepartmentCity,
    DateTime StartDate,
    DateTime EndDate);