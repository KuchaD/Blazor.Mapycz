using Blazor.Mapy.cz.Helper;

namespace Blazor.Mapy.cz;

public class Language : SmartEnum<Language, string>
{
    public static readonly Language Czech = new ("Czech", "cs");
    public static readonly Language Slovakia = new ("Slovakia", "sk");
    public static readonly Language English = new ("English", "en");
    public static readonly Language Germany = new ("Germany", "de");
    public static readonly Language Polish = new ("Polish", "pl");

    public Language(string name, string value) 
        : base(name, value)
    {
    }
    
    public static implicit operator string(Language smartEnum)
    {
        return smartEnum.Value;
    }
}
