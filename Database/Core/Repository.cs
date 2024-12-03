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

    public IEnumerable<T> Find(Predicate isNeed)
    {
        return List.Where(x => isNeed(x));
    }

    public void Remove(Predicate predicate)
    {
        var candidates = Find(predicate).ToList();
        if (candidates.Count == 0)
        {
            DbContext.Events.OnNotRemoved($"Not found: {typeof(T).Name} to remove.");
        }
        else
        {
            candidates.ForEach(x =>
            {
                _list.Remove(x);
                if (x != null) DbContext.Events.OnRemoved(x);
            });
            Save();
        }
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

    public void Update(Predicate isNeed, T item)
    {
        var candidates = Find(isNeed).ToList();
        if (candidates.Count == 0)
        {
            DbContext.Events.OnNotUpdated($"Not found: {typeof(T).Name} to update.");
        }
        else
        {
            candidates.ForEach(x =>
            {
                _list.Find(x)!.Value = item;
                if (item != null) DbContext.Events.OnUpdated(x);
            });
            Save();
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