namespace SigmaCars.WebApi.Features.CarType.RequestsAndResponses;

public record PostRentalRequest(
    int UserId,
    DateTime StartDate,
    DateTime EndDate);

// TODO: Add validation to requests that are not in mediator pipeline