using MediatR;

namespace SigmaCars.WebApi.Features.Car.Commands;

public record DeleteCarCommand(int Id) : IRequest;