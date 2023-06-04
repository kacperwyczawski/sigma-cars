namespace SigmaCars.WebApi.Features.CarType.RequestsAndResponses;

public record CreateCarTypeRequest(
        string Make,
        string Model,
        int ProductionYear,
        float PricePerDay,
        int SeatCount,
        IFormFile Image);