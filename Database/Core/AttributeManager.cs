using System.Reflection;
using Database.Utils;
using Attribute = Database.Attributes.Attribute;

namespace Database.Core;

public class AttributeManager
{
    private List<Type> _types = [];

    public AttributeManager()
    {
        CollectTypes();
        ExecuteAttributes();
    }

    private void CollectTypes()
    {
        _types = TypesUtil.Types;
    }

    private void ExecuteAttributes()
    {
        _types.ForEach(ProcessType);
    }

    private static void ProcessType(Type type)
    {
        var attributeProviders = new LinkedList<ICustomAttributeProvider>([type]);
        type.GetMembers().ToList().ForEach(x => attributeProviders.AddLast(x));
        attributeProviders.ToList().ForEach(ProcessAttributeProvider);
    }

    private static void ProcessAttributeProvider(ICustomAttributeProvider provider)
    {
        var attributes = provider.GetCustomAttributes(true);
        foreach (var attribute in attributes)
        {
            var customAttribute = attribute as Attribute;
            customAttribute?.Initialize(provider);
            customAttribute?.Process();
        }
    }
}