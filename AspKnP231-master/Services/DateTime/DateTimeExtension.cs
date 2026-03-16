using Microsoft.Extensions.DependencyInjection;

namespace AspKnP231.Services.DateTime;

public static class DateTimeExtension
{
    public static IServiceCollection AddDateTimeServices(this IServiceCollection services)
    {
      //  return services.AddScoped<IDateTimeService>();
        return services.AddScoped<NationalDateTimeService>();
    }
}