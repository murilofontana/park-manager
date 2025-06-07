using ParkManager.Middleware;

namespace ParkManager.Extensions
{
  internal static class ApplicationBuilderExtensions
  {

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
      app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
  }
}
