using FluentValidation;

namespace SigmaCars.WebApi.Features.Authentication.Queries;

public class LoginValidator : AbstractValidator<LoginQuery>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(100)
            .EmailAddress();
        RuleFor(x => x.Password)
            .MaximumLength(100)
            .NotEmpty();
    }
}