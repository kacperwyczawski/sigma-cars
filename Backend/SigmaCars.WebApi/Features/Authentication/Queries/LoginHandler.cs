using MediatR;
using SigmaCars.Domain.Exceptions;
using SigmaCars.WebApi.Features.Authentication.Shared;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Authentication.Queries;

public class LoginHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly JwtGenerator _jwtGenerator;
    private readonly SigmaCarsDbContext _dbContext;

    public LoginHandler(JwtGenerator jwtGenerator, SigmaCarsDbContext dbContext)
    {
        _jwtGenerator = jwtGenerator;
        _dbContext = dbContext;
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
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role,
                token));
    }
}