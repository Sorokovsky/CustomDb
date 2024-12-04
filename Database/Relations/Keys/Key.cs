using System.Reflection;

namespace Database.Relations.Keys;

public class Key
{
    private Key(KeyTypes type, PropertyInfo property)
    {
        Type = type;
        Property = property;
    }

    public KeyTypes Type { get; private set; }

    public PropertyInfo Property { get; private set; }

    public static Key CreatePrimaryKey(PropertyInfo property)
    {
        return new Key(KeyTypes.Primary, property);
    }

    public static Key CreateForeignKey(PropertyInfo property)
    {
        return new Key(KeyTypes.Foreign, property);
    }
}