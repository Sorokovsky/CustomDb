using System.Reflection;
using Database.Relations.Keys;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    public override void Process()
    {
        var property = ConvertMemberToProperty();
        var key = Key.CreatePrimaryKey(property.Name, ParentType!.Name);
        var manager = new KeysManager();
        manager.TryAddKey(key);
    }

    private PropertyInfo ConvertMemberToProperty()
    {
        return (PropertyInfo)Member!;
    }
}