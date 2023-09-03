namespace Blazor.Mapy.cz.Models;

public class GeoCodeResponse
{
    public List<Item> Items { get; set; }

    public GeoCodeResponse(List<Item> items)
    {
        Items = items;
    }

    public GeoCodeResponse()
    {
        Items = new List<Item>();
    }
};

public class Item
{
    public string Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public Position Position { get; set; } = new ();
    public string Type { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public List<RegionalStructure> RegionalStructure { get; set; } = new ();
    public string? Zip { get; set; } 
    public string? Locality { get; set; } 
}

public class Position
{
    public double Lon { get; set; }
    public double Lat { get; set; }
}

public class RegionalStructure
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
};