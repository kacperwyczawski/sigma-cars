using MediatR;

namespace SigmaCars.Application.Features.Authentication.Queries;

public record LoginQuery(
    string Email,
    string Password) 
    : IRequest<LoginResponse>;