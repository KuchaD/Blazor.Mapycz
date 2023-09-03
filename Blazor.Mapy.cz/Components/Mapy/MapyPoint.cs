using System.Globalization;

namespace Blazor.Mapy.cz.Components.Mapy;

public record MapyPoint(double Lat, double Lng)
{
    public override string ToString()
    {
        return $"{Lng.ToString(CultureInfo.InvariantCulture.NumberFormat)},{Lat.ToString(CultureInfo.InvariantCulture.NumberFormat)}";
    }
};