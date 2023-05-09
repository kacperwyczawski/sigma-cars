using FluentValidation;

namespace SigmaCars.WebApi.Features.Authentication.Commands;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(100)
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(100);
    }
}