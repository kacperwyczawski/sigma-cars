using MediatR;

namespace SigmaCars.Application.Features.Authentication.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<RegisterResponse>;