namespace SigmaCars.WebApi.Features.CarType.Requests;

public record PostRentalRequest(
    int UserId,
    DateTime StartDate,
    DateTime EndDate);

// TODO: Add validation