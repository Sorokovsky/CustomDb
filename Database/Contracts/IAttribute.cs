using System.Reflection;

namespace Database.Contracts;

public interface IAttribute
{
    public void Process(MemberInfo property, Type? instanceType = null);
}