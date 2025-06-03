using FluentValidation;
using ParkManager.Application.Establishments.Create;

namespace ParkManager.Application.Establishments.Update;

internal class UpdateEstablishmentCommandValidator : AbstractValidator<UpdateEstablishmentCommand>
{
  public UpdateEstablishmentCommandValidator()
  {
    RuleFor(x => x)
        .NotNull().WithMessage("Establishment data cannot be null.");

    RuleFor(x => x.Id)
        .NotEmpty().WithMessage("Id is required.");

    RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required.");

    RuleFor(x => x.Cnpj)
        .NotEmpty().WithMessage("Cnpj is required.")
        .Length(14).WithMessage("Cnpj must be 14 digits.")
        .Matches(@"^\d{14}$").WithMessage("Cnpj must contain only numbers.");

    RuleFor(x => x.City)
        .NotEmpty().WithMessage("City is required.");

    RuleFor(x => x.State)
        .NotEmpty().WithMessage("State is required.")
        .Length(2).WithMessage("State must be the 2-letter UF code.")
        .Matches(@"^[A-Z]{2}$").WithMessage("State must be uppercase 2-letter UF code.");

    RuleFor(x => x.Street)
        .NotEmpty().WithMessage("Street is required.");

    RuleFor(x => x.Number)
        .NotEmpty().WithMessage("Number is required.");

    RuleFor(x => x.Complement)
        .MaximumLength(50).WithMessage("Complement must be at most 50 characters.");

    RuleFor(x => x.ZipCode)
        .NotEmpty().WithMessage("ZipCode is required.")
        .Length(8).WithMessage("ZipCode must be 8 digits.")
        .Matches(@"^\d{8}$").WithMessage("ZipCode must contain only numbers.");

    RuleFor(x => x.Phone)
        .NotEmpty().WithMessage("Phone is required.")
        .MaximumLength(20).WithMessage("Phone must be at most 20 characters.");

    RuleFor(x => x.MotorcyclesParkingSpaces)
        .GreaterThanOrEqualTo(0).WithMessage("MotorcyclesParkingSpaces must be zero or positive.");

    RuleFor(x => x.CarsParkingSpaces)
        .GreaterThanOrEqualTo(0).WithMessage("CarsParkingSpaces must be zero or positive.");

    RuleFor(x => x)
        .Must(x => x.MotorcyclesParkingSpaces > 0 || x.CarsParkingSpaces > 0)
        .WithMessage("At least one type of parking space must be greater than zero.");
  }
}
