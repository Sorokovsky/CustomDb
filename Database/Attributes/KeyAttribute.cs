namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class KeyAttribute : Attribute
{
    
    public override void Process()
    {
    }
}