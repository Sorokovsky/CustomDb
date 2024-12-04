using System.Reflection;
using Database.Relations.Keys;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class KeyAttribute : Attribute
{
    public override void Process()
    {
        var property = GetProperty(Member!);
        var key = Key.CreatePrimaryKey(property);
    }

    private PropertyInfo GetProperty(MemberInfo member)
    {
        return (PropertyInfo)member;
    }
}