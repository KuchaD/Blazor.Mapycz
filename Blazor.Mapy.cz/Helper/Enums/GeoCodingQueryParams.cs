using Blazor.Mapy.cz.Helper.Enums;

namespace Blazor.Mapy.cz.Helper;

public class GeoCodingQueryParams : QueryParams
{
    public static readonly GeoCodingQueryParams Search = new ("Search", "query");
    public static readonly GeoCodingQueryParams Type = new ("Type", "type");
    public static readonly GeoCodingQueryParams Limit = new ("Limit", "limit");

    public GeoCodingQueryParams(string name, string value) : base(name, value)
    {
    }
    
    public static implicit operator string(GeoCodingQueryParams smartEnum)
    {
        return smartEnum.Value;
    }
}