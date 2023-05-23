using MediatR;

namespace SigmaCars.WebApi.Features.Car.Commands;

public record CreateCarCommand(
        int carTypeId,
        int DepartmentId,
        string RegistrationNumber,
        string Vin) // TODO: remove vin
    : IRequest<CreateCarResponse>;