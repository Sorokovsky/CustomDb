using Database.Events;
using Database.Indexing;

namespace Database.Core;

public abstract class DbContext
{
    private readonly AttributeManager _attributeManager;

    private IndexingManager _indexing;

    protected DbContext()
    {
        _indexing = IndexingManager.Singleton();
        _attributeManager = new AttributeManager();
    }

    public static DbEvents Events { get; private set; } = new();
}