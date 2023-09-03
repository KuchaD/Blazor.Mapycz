using Blazor.Mapy.cz.Components.Mapy;
using Blazor.Mapy.cz.Helper.Enums;
using Blazor.Mapy.cz.Models;

namespace Blazor.Mapy.cz.Services;

public interface IRouteService
{
    public Task<RouteResponse> GetRoute(
        MapyPoint start, 
        MapyPoint end, 
        RouteType routeType, 
        bool avoidToll = false, 
        List<MapyPoint>? waypoints = null, 
        CancellationToken cancellationToken = default);
}