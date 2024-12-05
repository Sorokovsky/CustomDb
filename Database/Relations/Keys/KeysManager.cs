using Database.Contracts;
using Database.Storages;

namespace Database.Relations.Keys;

public class KeysManager
{
    private const string Folder = "database/keys";
    private readonly LinkedList<Key> _emptyList = [];

    public void AddKey(Key key)
    {
        var keys = GetKeys(key.Type);
        keys.AddLast(key);
        SaveKeys(keys, key.Type);
    }

    public LinkedList<Key> GetKeys(KeyTypes type)
    {
        var result = FileStorage<LinkedList<Key>>.LoadFromFile(Folder, PrepareFilePath(type));
        return result ?? _emptyList;
    }

    public void RemoveKey(Key key)
    {
        var keys = GetKeys(key.Type);
        keys.Remove(key);
        SaveKeys(keys, key.Type);
    }

    private static string PrepareFilePath(KeyTypes type)
    {
        return $"{type.ToString()}.dat";
    }

    private static void SaveKeys(LinkedList<Key> keys, KeyTypes type)
    {
        FileStorage<LinkedList<Key>>.SaveToFile(Folder, PrepareFilePath(type), keys);
    }
}