using FluentValidation;

namespace ParkManager.Application.Vehicles.Create;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
  public CreateVehicleCommandValidator()
  {
    RuleFor(x => x.Model)
        .NotEmpty().WithMessage("Model is required.")
        .MaximumLength(100).WithMessage("Model must not exceed 100 characters.");
    RuleFor(x => x.Plate)
        .NotEmpty().WithMessage("Plate is required.")
        .MaximumLength(20).WithMessage("Plate must not exceed 20 characters.");
    RuleFor(x => x.Color)
        .NotEmpty().WithMessage("Color is required.")
        .MaximumLength(50).WithMessage("Color must not exceed 50 characters.");
    RuleFor(x => x.Type)
        .IsInEnum().WithMessage("Type must be a valid vehicle type.");
  }
}