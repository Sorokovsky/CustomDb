using System.Reflection;

namespace Database.Attributes;

public abstract class Attribute : System.Attribute
{
    protected MemberInfo? Member { get;  private set; }
    
    protected Type? ParentType { get;  private set; }
    
    public abstract void Process();

    public void Initialize(ICustomAttributeProvider provider)
    {
        if (provider is MemberInfo member)
        {
            ParentType = member.DeclaringType;
            Member = member;
        }
        else
        {
            ParentType = provider.GetType();
            Member = null;
        }
    }
}