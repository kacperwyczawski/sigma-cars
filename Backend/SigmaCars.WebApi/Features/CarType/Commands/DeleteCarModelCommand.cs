using MediatR;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public record DeletecarTypeCommand(int Id) : IRequest;