namespace SigmaCars.WebApi.Features.Rental.Queries;

public record GetRentalsResponse(
    IEnumerable<GetRentalResponse> Rentals);

public record GetRentalResponse(
    string CarMake,
    string carType,
    string CarRegistrationNumber,
    DateTime StartDate,
    DateTime EndDate);