using Blazor.Mapy.cz.JsInterop;
using Blazor.Mapy.cz.Options;
using Blazor.Mapy.cz.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Mapy.cz;

public static class ServicesExtensions
{
    public static IServiceCollection AddMapyCz(this IServiceCollection services, Action<MapyCzOptions> configureOptions)
    {
        services.AddOptions();
        services.Configure(configureOptions);
        services.AddScoped<SetUpMapJs>();
        services.AddScoped<SetMarkerJs>();
        services.AddScoped<RouteMapJs>();
        services.AddHttpClient<IGeocodeService, GeocodeService>(opt =>
        {
            opt.BaseAddress = new Uri("https://api.mapy.cz/");
        });
        
        services.AddHttpClient<IRouteService, RouteService>(opt =>
        {
            opt.BaseAddress = new Uri("https://api.mapy.cz/");
        });
        
        return services;
    }
}