using System.Collections.Concurrent;
using System.Reflection;

namespace Blazor.Mapy.cz.Helper;

public abstract class SmartEnum<TEnum, TValue> where TEnum : SmartEnum<TEnum, TValue>
{
    private static readonly ConcurrentDictionary<TValue, TEnum> _items = new ();

    public TValue Value { get; }
    public string Name { get; }

    protected SmartEnum(string name, TValue value)
    {
        Value = value;
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}
