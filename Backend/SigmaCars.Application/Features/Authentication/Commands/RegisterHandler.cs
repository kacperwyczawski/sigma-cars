using MediatR;
using SigmaCars.Application.Persistence;
using SigmaCars.Domain.Exceptions;
using SigmaCars.Domain.Models;

namespace SigmaCars.Application.Features.Authentication.Commands;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly SigmaCarsDbContext _dbContext;
    private readonly IJwtGenerator _jwtGenerator;

    public RegisterHandler(
        SigmaCarsDbContext dbContext,
        IJwtGenerator jwtGenerator)
    {
        _dbContext = dbContext;
        _jwtGenerator = jwtGenerator;
    }

    public Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_dbContext.Users.Any(u => u.Email == request.Email))
            throw new ConflictException($"User with {request.Email} email already exists");

        var user = new User
        {
            Id = 0,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = request.Password, // TODO: Hash password
            Role = UserRole.Customer,
            Rentals = new List<Rental>()
        };

        var token = _jwtGenerator.GenerateToken(user);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Task.FromResult(
            new RegisterResponse(
                user.FirstName,
                user.LastName,
                user.Email,
                token));
    }
}