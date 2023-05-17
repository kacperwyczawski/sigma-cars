namespace SigmaCars.WebApi.Features.CarModel.Requests;

public record PostRentalRequest(
    int UserId,
    DateTime StartDate,
    DateTime EndDate);

// TODO: Add validation