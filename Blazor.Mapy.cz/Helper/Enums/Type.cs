namespace Blazor.Mapy.cz.Helper.Enums;

public class Type : SmartEnum<Type, string>
{
    public static readonly Type Regional = new RegionalSubType();
    public static readonly Type Poi = new PoiType();

    public Type(string name, string value) 
        : base(name, value)
    {
    }
    
    public sealed class RegionalSubType : Type
    {
        private static string Name => "Regional";
        private static string Value => "regional";
        
        public static Type Municipality = new ($"{Name}.Municipality", $"{Value}.municipality");
        public static Type Street = new ($"{Name}.Street", $"{Value}.street");
        public static Type Address = new ($"{Name}.Address", $"{Value}.address");
        public static Type MunicipalityPart  = new ($"{Name}.MunicipalityPart ", $"{Value}.municipality_part");
        public static Type Region  = new ($"{Name}.Region ", $"{Value}.region");
        public static Type Country  = new ($"{Name}.Country ", $"{Value}.country");
        
        public RegionalSubType() : base(Name, Value) {}
    }
    
    private sealed class PoiType : Type
    {
        private static string Name => "Poi";
        private static string Value => "Poi";
        
        public PoiType() : base(Name, Value) {}
    }
    
    public static implicit operator string(Type smartEnum)
    {
        return smartEnum.Value;
    }
    
}




