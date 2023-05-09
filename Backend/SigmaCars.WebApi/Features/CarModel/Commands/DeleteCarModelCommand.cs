using MediatR;

namespace SigmaCars.WebApi.Features.CarModel.Commands;

public record DeleteCarModelCommand(int Id) : IRequest;