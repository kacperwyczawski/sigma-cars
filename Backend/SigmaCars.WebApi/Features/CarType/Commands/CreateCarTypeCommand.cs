using MediatR;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public record CreateCarTypeCommand(
        string Make,
        string Model,
        int ProductionYear,
        string Color,
        float PricePerDay,
        int SeatCount)
    : IRequest<CreateCarTypeResponse>;