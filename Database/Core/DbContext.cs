using Database.Events;
using Database.Indexing;

namespace Database.Core;

public abstract class DbContext
{
    
    public static DbEvents Events { get; private set; } = new();

    private IndexingManager _indexing;

    private DbContext()
    {
        _indexing = new IndexingManager();
    }
}