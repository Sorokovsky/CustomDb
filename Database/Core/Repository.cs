using Database.Contracts;
using Database.Storages;

namespace Database.Core;

public class Repository<T>
{
    private readonly IStorage<LinkedList<T>> _storage;
    private LinkedList<T> _list;

    public Repository(string filePath)
    {
        _storage = new FileStorage<LinkedList<T>>(filePath);
        _list = [];
        Load();
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