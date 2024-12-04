using Database.Events;

namespace Database.Core;

public abstract class DbContext
{
    private readonly AttributeManager _attributeManager;

    protected DbContext()
    {
        _attributeManager = new AttributeManager();
    }

    public static DbEvents Events { get; private set; } = new();
}