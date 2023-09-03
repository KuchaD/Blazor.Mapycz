namespace Blazor.Mapy.cz.Helper.Enums;

public class RouteType : SmartEnum<RouteType, string>
{
    public static readonly RouteType CarFast = new ("CarFast", "car_fast");
    public static readonly RouteType CarFastTraffic = new ("CarFastTraffic", "car_fast_traffic");
    public static readonly RouteType CarShort = new ("CarShort", "car_short");
    public static readonly RouteType FootFast = new ("FootFast", "foot_fast");
    public static readonly RouteType BikeRoad = new ("BikeRoad", "bike_road");
    public static readonly RouteType BikeMountain = new ("BikeMountain", "bike_mountain");

    public RouteType(string name, string value) : base(name, value)
    {
    }
    
    public static implicit operator string(RouteType smartEnum)
    {
        return smartEnum.Value;
    }
}