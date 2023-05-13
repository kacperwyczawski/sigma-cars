using MediatR;

namespace SigmaCars.WebApi.Features.Car.Queries;

public record GetCarsQuery(int CarModelId) : IRequest<GetCarsResponse>;