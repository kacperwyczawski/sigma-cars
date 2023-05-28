using MediatR;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public record DeleteCarTypeCommand(int Id) : IRequest;