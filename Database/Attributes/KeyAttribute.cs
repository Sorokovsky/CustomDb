using System.Reflection;
using Database.Contracts;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class KeyAttribute : Attribute, IAttribute
{
    public void Process(MemberInfo property, Type? instanceType = null)
    {
    }
}