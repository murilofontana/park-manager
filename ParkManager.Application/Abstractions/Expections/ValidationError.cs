namespace ParkManager.Application.Abstractions.Expections;
public sealed record ValidationError(string PropertyName, string ErrorMessage);