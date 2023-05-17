namespace SigmaCars.WebApi.Features.Rental.Queries;

public record GetRentalsResponse(
    IEnumerable<GetRentalResponse> Rentals);

public record GetRentalResponse(
    string CarMake,
    string CarType,
    string CarRegistrationNumber,
    DateTime StartDate,
    DateTime EndDate);