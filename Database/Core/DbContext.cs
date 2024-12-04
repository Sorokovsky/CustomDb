using Database.Events;
using Database.Indexing;

namespace Database.Core;

public abstract class DbContext
{
    private readonly AttributeManager _attributeManager;

    private IndexingManager _indexing;

    protected DbContext()
    {
        _indexing = IndexingManager.Instance;
        _attributeManager = new AttributeManager();
    }

    public static DbEvents Events { get; private set; } = new();
}