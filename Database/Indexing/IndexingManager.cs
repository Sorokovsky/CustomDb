using Database.Core;

namespace Database.Indexing;

public class IndexingManager
{
    public delegate bool IsNeed(Index index);

    private static IndexingManager? _instance;

    private readonly LinkedList<Index> _indexes;

    private IndexingManager()
    {
        _indexes = [];
    }

    public static IndexingManager Singleton()
    {
        if (_instance == null) _instance = new IndexingManager();
        return _instance;
    }

    public List<IndexUnit> GetDependencies(Type type)
    {
        return GetBy(type, index => index.DependsOn, index => index.Dependency);
    }

    public List<IndexUnit> GetDependsOn(Type type)
    {
        return GetBy(type, index => index.Dependency, index => index.DependsOn);
    }

    public void Remove(IsNeed isNeed)
    {
        var candidates = _indexes.Where(x => isNeed(x)).ToList();
        if (candidates.Count == 0)
            DbContext.Events.OnNotRemoved("No one index found.");
        else
            candidates.ForEach(x =>
            {
                _indexes.Remove(x);
                DbContext.Events.OnRemoved(x);
            });
    }

    public void Add(Index index)
    {
        _indexes.AddLast(index);
        DbContext.Events.OnCreated(index);
    }

    private List<IndexUnit> GetBy(Type type, SelectUnit selectSource, SelectUnit selectResult)
    {
        return _indexes
            .Where(index => selectSource(index).Type.FullName!.Equals(type.FullName))
            .Select(index => selectResult(index))
            .ToList();
    }

    private delegate IndexUnit SelectUnit(Index index);
}