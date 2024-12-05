using Database.Contracts;

namespace Database.Relations.Keys;

public class Key
{
    public Key(KeyTypes type, string property, string parent)
    {
        Type = type;
        Property = property;
        Parent = parent;
    }

    public KeyTypes Type { get; private set; }

    public string Property { get; private set; }

    public string Parent { get; private set; }

    public static Key CreatePrimaryKey(string property, string parent)
    {
        return new Key(KeyTypes.Primary, property, parent);
    }

    public static Key CreateForeignKey(string property, string parent)
    {
        return new Key(KeyTypes.Foreign, property, parent);
    }
}