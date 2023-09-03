namespace Blazor.Mapy.cz.Helper.Enums;

public class RouteQueryParams : SmartEnum<RouteQueryParams, string>
{
    public static readonly RouteQueryParams Start = new ("Start", "start");
    public static readonly RouteQueryParams End = new ("End", "end");
    public static readonly RouteQueryParams RouteType = new ("RouteType", "routeType");
    public static readonly RouteQueryParams AvoidToll = new ("AvoidToll", "avoidToll");
    public static readonly RouteQueryParams Waypoints = new ("Waypoints", "waypoints");
    public RouteQueryParams(string name, string value) : base(name, value)
    {
    }
    
    public static implicit operator string(RouteQueryParams smartEnum)
    {
        return smartEnum.Value;
    }
}