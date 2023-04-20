using MediatR;

namespace SigmaCars.Application.Features.CarModel.Commands;

public record CreateCarModelCommand(
        string Make,
        string Model,
        int ProductionYear,
        string Color,
        float PricePerDay,
        int SeatCount)
    : IRequest<CreateCarModelResponse>;