namespace SigmaCars.WebApi.Features.Rental.Queries;

public record GetRentalsResponse(
    IEnumerable<GetRentalResponse> Rentals);

public record GetRentalResponse(
    int Id,
    string CarMake,
    string CarModel,
    string CarRegistrationNumber,
    string DepartmentCity,
    DateTime StartDate,
    DateTime EndDate);