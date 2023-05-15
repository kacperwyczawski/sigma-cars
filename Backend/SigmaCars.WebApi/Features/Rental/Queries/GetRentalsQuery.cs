using MediatR;

namespace SigmaCars.WebApi.Features.Rental.Queries;

public record GetRentalsQuery(
    int UserId)
    : IRequest<GetRentalsResponse>;