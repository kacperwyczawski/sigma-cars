using MediatR;

namespace SigmaCars.WebApi.Features.Car.Queries;

public record GetCarsQuery(int CarTypeId) : IRequest<GetCarsResponse>; //TODO: fix naming and namespaces/names of other objects