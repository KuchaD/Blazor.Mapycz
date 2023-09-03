using Blazor.Mapy.cz.Components.Mapy;
using Blazor.Mapy.cz.Helper;
using Blazor.Mapy.cz.Helper.Enums;
using Blazor.Mapy.cz.Models;
using Blazor.Mapy.cz.Options;
using Microsoft.Extensions.Options;

namespace Blazor.Mapy.cz.Services;

public class RouteService : IRouteService
{
    private readonly FluentClient _fluentClient;
    private readonly MapyCzOptions _options;

    public RouteService(HttpClient client, IOptions<MapyCzOptions> options)
    {
        _options = options.Value;
        _fluentClient = new FluentClient(client);
    }
    
    public async Task<RouteResponse> GetRoute(MapyPoint start, MapyPoint end, RouteType routeType, bool avoidToll = false,
        List<MapyPoint>? waypoints = null, CancellationToken cancellationToken = default)
    {
        var builder = _fluentClient
            .Get("v1/routing/route")
            .AddQueryParameter(QueryParams.Apikey, _options.ApiKey)
            .AddQueryParameter(QueryParams.Lang, _options.Lang)
            .AddQueryParameter(RouteQueryParams.Start, start.ToString())
            .AddQueryParameter(RouteQueryParams.End, end.ToString())
            .AddQueryParameter(RouteQueryParams.RouteType, routeType)
            .AddQueryParameter(RouteQueryParams.AvoidToll, avoidToll.ToString().ToLower())
            .WithCancellationToken(cancellationToken);
        
        if (waypoints != null)
        {
            foreach (var waypoint in waypoints)
            {
                builder.AddQueryParameter(RouteQueryParams.Waypoints, waypoint.ToString());
            }
        }
        
        return await builder.SendAsync<RouteResponse>();
    }
}