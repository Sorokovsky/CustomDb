using System.Reflection;
using Database.Contracts;

namespace Database.Core;

public class AttributeManager
{
    private List<Type> _types;

    public void CollectTypes()
    {
        _types = new List<Type>();
        AppDomain.CurrentDomain.GetAssemblies()
            .Select(x => x.GetTypes())
            .ToList().ForEach(a => a.ToList().ForEach(x => _types.Add(x)));
    }

    public void ExecuteAttributes()
    {
        foreach (var type in _types)
        {
            var members = type.GetMembers();
            foreach (var member in members)
            {
                var attributes = member.CustomAttributes;
                foreach (var attribute in attributes)
                    if (IsCustom(attribute.AttributeType))
                        ProcessAttribute(attribute.AttributeType, member, type);
            }
        }
    }

    private static bool IsCustom(Type attribute)
    {
        var attributeInterface = attribute
            .GetInterfaces()
            .FirstOrDefault(x => x.Name == nameof(IAttribute));
        return attributeInterface != null;
    }

    private static void ProcessAttribute(Type attributeType, MemberInfo member, Type? parent = null)
    {
        var attribute = (IAttribute)Activator.CreateInstance(attributeType)!;
        attribute?.Process(member, parent);
    }
}