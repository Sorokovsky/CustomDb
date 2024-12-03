namespace Database.Indexing;

public class IndexingSystem
{
    private readonly LinkedList<Index> _indexes = [];

    private delegate IndexUnit SelectUnit(Index index);

    public List<IndexUnit> GetDependencies(Type type)
    {
        return GetBy(type, index => index.DependsOn, index => index.Dependency);
    }

    public List<IndexUnit> GetDependsOn(Type type)
    {
        return GetBy(type, index => index.Dependency, index => index.DependsOn);
    }

    private List<IndexUnit> GetBy(Type type, SelectUnit selectSource, SelectUnit selectResult)
    {
        return _indexes
            .Where(index => selectSource(index).Type.FullName!.Equals(type.FullName))
            .Select(index => selectResult(index))
            .ToList();
    }
}