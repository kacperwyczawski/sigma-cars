using MediatR;

namespace SigmaCars.WebApi.Features.Authentication.Queries;

public record LoginQuery(
    string Email,
    string Password) 
    : IRequest<LoginResponse>;