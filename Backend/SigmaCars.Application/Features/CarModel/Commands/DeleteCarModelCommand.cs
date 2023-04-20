using MediatR;

namespace SigmaCars.Application.Features.CarModel.Commands;

public record DeleteCarModelCommand(int Id) : IRequest;