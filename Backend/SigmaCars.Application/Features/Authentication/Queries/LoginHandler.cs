using MediatR;
using Microsoft.Extensions.Logging;
using SigmaCars.Application.Persistence;
using SigmaCars.Domain.Exceptions;

namespace SigmaCars.Application.Features.Authentication.Queries;

public class LoginHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly SigmaCarsDbContext _dbContext;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(IJwtGenerator jwtGenerator, SigmaCarsDbContext dbContext, ILogger<LoginHandler> logger)
    {
        _jwtGenerator = jwtGenerator;
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null)
            throw new InvalidCredentialsException($"Email ({request.Email}) or password is incorrect");

        if (user.PasswordHash != request.Password)
            throw new InvalidCredentialsException($"Email ({request.Email}) or password is incorrect");

        var token = _jwtGenerator.GenerateToken(user);

        return Task.FromResult(
            new LoginResponse(
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email,
                Jwt: token));
    }
}