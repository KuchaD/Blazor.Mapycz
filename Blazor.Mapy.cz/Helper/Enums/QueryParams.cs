namespace Blazor.Mapy.cz.Helper.Enums;

public class QueryParams : SmartEnum<QueryParams, string>
{
    public static readonly QueryParams Apikey = new ("Apikey", "apikey");
    public static readonly QueryParams Lang = new ("Lang", "lang");

    public QueryParams(string name, string value) : base(name, value)
    {
    }
    
    public static implicit operator string(QueryParams smartEnum)
    {
        return smartEnum.Value;
    }
}