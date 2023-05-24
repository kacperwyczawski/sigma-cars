// using FluentValidation;
//
// namespace SigmaCars.WebApi.Features.Car.Commands;
//
// public class CreateCarValidator : AbstractValidator<CreateCarCommand>
// {
//     public CreateCarValidator()
//     {
//         RuleFor(x => x.CarTypeId)
//             .NotEmpty();
//         RuleFor(x => x.DepartmentId)
//             .NotEmpty();
//         RuleFor(x => x.RegistrationNumber)
//             .NotEmpty()
//             .MaximumLength(10);
//         RuleFor(x => x.Vin)
//             .NotEmpty()
//             .MaximumLength(17)
//             .Must(vin => !vin.Contains('I') && !vin.Contains('O') && !vin.Contains('Q'))
//             .WithMessage("VIN cannot contain the letters I, O, or Q.");
//     }
// }
// 