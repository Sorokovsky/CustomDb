using Database.Events;
using Database.Indexing;

namespace Database.Core;

public abstract class DbContext
{
    private readonly AttributeManager _attributeManager;

    private IndexingManager _indexing;

    protected DbContext()
    {
        _indexing = new IndexingManager();
        _attributeManager = new AttributeManager();
        _attributeManager.CollectTypes();
        _attributeManager.ExecuteAttributes();
    }

    public static DbEvents Events { get; private set; } = new();
}