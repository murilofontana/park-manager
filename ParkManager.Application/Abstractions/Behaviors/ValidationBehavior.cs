﻿using FluentValidation;
using MediatR;
using ParkManager.Application.Abstractions.Expections;
using System;

namespace ParkManager.Application.Abstractions.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    if (!_validators.Any())
    {
      return await next();
    }

    var context = new ValidationContext<TRequest>(request);

    var validationErrors = _validators
        .Select(validator => validator.Validate(context))
        .Where(validationResult => validationResult.Errors.Any())
        .SelectMany(validationResult => validationResult.Errors)
        .Select(validationFailure => new ValidationError(
            validationFailure.PropertyName,
            validationFailure.ErrorMessage))
    .ToList();

    if (validationErrors.Any())
    {
      throw new Expections.ValidationException(validationErrors);
    }

    return await next();
  }
}
