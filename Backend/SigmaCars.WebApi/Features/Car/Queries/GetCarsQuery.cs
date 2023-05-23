using MediatR;

namespace SigmaCars.WebApi.Features.Car.Queries;

public record GetCarsQuery(int carTypeId) : IRequest<GetCarsResponse>; //TODO: fix naming and namespaces/names of other objects