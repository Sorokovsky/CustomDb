using Database.Contracts;
using Database.Storages;

namespace Database.Core;

public class Repository<T>
{
    public delegate bool Predicate(T input);

    private readonly IStorage<LinkedList<T>> _storage;
    private LinkedList<T> _list = [];

    public Repository(string filePath)
    {
        _storage = new FileStorage<LinkedList<T>>(filePath);
        Load();
    }

    public IReadOnlyList<T> List => _list.ToList();

    public T Add(T item)
    {
        var addedItem = _list.AddLast(item).Value;
        Save();
        if (item != null) DbContext.Events.OnCreated(item);
        return addedItem;
    }

    public IEnumerable<T> Find(Predicate predicate)
    {
        return _list.Where(x => predicate(x));
    }

    public void Remove(Predicate predicate)
    {
        var candidates = Find(predicate).ToList();
        if (candidates.Count == 0)
        {
            DbContext.Events.OnNotRemoved($"Not found: {typeof(T).Name} to remove.");
            return;
        }

        candidates.ForEach(x =>
        {
            _list.Remove(x);
            if (x != null) DbContext.Events.OnRemoved(x);
        });
        Save();
    }

    public void Sort(Comparer<T> comparer)
    {
        try
        {
            var sorted = _list.ToList();
            sorted.Sort(comparer);
            _list = new LinkedList<T>(sorted);
            DbContext.Events.OnSorted(List.Count);
            Save();
        }
        catch (Exception e)
        {
            DbContext.Events.OnNotSorted(e.Message);
        }
    }

    private void Save()
    {
        _storage.Save(_list);
    }

    private void Load()
    {
        _list = _storage.Load() ?? new LinkedList<T>();
    }
}