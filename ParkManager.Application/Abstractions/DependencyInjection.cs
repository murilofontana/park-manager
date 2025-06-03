using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ParkManager.Application.Abstractions.Behaviors;

namespace ParkManager.Application.Abstractions;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(configuration =>
    {
      //Get all the services from assembly and inject them
      configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

      configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });

    //Get all the validator from assembly and inject them
    services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

    return services;
  }
}
