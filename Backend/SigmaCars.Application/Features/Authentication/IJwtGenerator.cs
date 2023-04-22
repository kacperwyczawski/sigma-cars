using SigmaCars.Domain.Models;

namespace SigmaCars.Application.Features.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}