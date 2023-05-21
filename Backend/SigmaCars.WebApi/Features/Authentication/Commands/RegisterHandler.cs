using MediatR;
using SigmaCars.Domain.Exceptions;
using SigmaCars.Domain.Models;
using SigmaCars.WebApi.Features.Authentication.Shared;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Authentication.Commands;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly SigmaCarsDbContext _dbContext;
    private readonly JwtGenerator _jwtGenerator;

    public RegisterHandler(
        SigmaCarsDbContext dbContext,
        JwtGenerator jwtGenerator)
    {
        _dbContext = dbContext;
        _jwtGenerator = jwtGenerator;
    }

    public Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_dbContext.Users.Any(u => u.Email == request.Email))
            throw new ConflictException($"User with {request.Email} email already exists");

        var user = new Domain.Models.User
        {
            Id = 0,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = request.Password, // TODO: Hash password
            Role = UserRole.Customer,
            Rentals = new List<Domain.Models.Rental>()
        };

        var token = _jwtGenerator.GenerateToken(user);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Task.FromResult(
            new RegisterResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token));
    }
}