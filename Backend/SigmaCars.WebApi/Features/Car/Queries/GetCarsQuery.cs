using MediatR;

namespace SigmaCars.WebApi.Features.Car.Queries;

public record GetCarsQuery(int carTypeId) : IRequest<GetCarsResponse>;