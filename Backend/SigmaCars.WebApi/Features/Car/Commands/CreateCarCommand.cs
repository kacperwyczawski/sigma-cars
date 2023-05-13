using MediatR;

namespace SigmaCars.WebApi.Features.Car.Commands;

public record CreateCarCommand(
        int CarModelId,
        int DepartmentId,
        string RegistrationNumber,
        string Vin)
    : IRequest<CreateCarResponse>;