using Database.Contracts;
using Database.Storages;

namespace Database.Relations.Keys;

public class KeysManager
{
    private const string Folder = "database/keys";
    private readonly LinkedList<Key> _emptyList = [];

    public bool TryAddKey(Key key)
    {
        var keys = GetKeys(key.Type);
        if (HasKey(keys, key) == false)
        {
            keys.AddLast(key);
            SaveKeys(keys, key.Type);
            return true;
        }

        return false;
    }

    private LinkedList<Key> GetKeys(KeyTypes type)
    {
        var result = FileStorage<LinkedList<Key>>.LoadFromFile(Folder, PrepareFilePath(type));
        return result ?? _emptyList;
    }

    public bool TryRemoveKey(Key key)
    {
        var keys = GetKeys(key.Type);
        if (HasKey(keys, key))
        {
            keys.Remove(key);
            SaveKeys(keys, key.Type);
            return true;
        }

        return false;
    }

    private static bool HasKey(LinkedList<Key> keys, Key key)
    {
        return keys.FirstOrDefault(x => x.Equals(key)) != null;
    }

    private static string PrepareFilePath(KeyTypes type)
    {
        return $"{type.ToString()}";
    }

    private static void SaveKeys(LinkedList<Key> keys, KeyTypes type)
    {
        FileStorage<LinkedList<Key>>.SaveToFile(Folder, PrepareFilePath(type), keys);
    }
}