using System.Text;
using System.Text.Json;

namespace Database;

public class Repository<T>
{
    private LinkedList<T> _list;
    private readonly string _filePath;

    public Repository(string filePath)
    {
        _filePath = filePath;
        _list = new LinkedList<T>();
    }

    private void Save()
    {
        var mode = FileMode.OpenOrCreate;
        if (File.Exists(_filePath))
        {
            mode = FileMode.Truncate;
        }
        var text = JsonSerializer.Serialize(_list, new JsonSerializerOptions { WriteIndented = true });
        using var stream = new StreamWriter(File.Open(_filePath, mode));
        stream.Write(text);
    }

    private void Load()
    {
        if (File.Exists(_filePath) == false) return;
        using var stream = new StreamReader(File.Open(_filePath, FileMode.Open));
        var text = stream.ReadToEnd();
        _list = JsonSerializer.Deserialize<LinkedList<T>>(text) ?? new LinkedList<T>();
    }
}