namespace Blazor.Mapy.cz.Models;

public class RouteResponse
{
    public double Length { get; set; }
    public double Duration { get; set; }
    public GeometryType Geometry { get; set; }

}

public class GeometryType
{
    public string Type { get; set; }
    public GeometryGeometry Geometry { get; set; }
    public object Properties { get; set; }
}

public class GeometryGeometry
{
    public string Type { get; set; }
    public List<List<double>> Coordinates { get; set; }
}