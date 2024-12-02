using System.Text.Json;
using Database.Contracts;

namespace Database.Storages;

public class FileStorage<T> : IStorage<T> where T : class
{
    private readonly string _filePath;

    public FileStorage(string filePath)
    {
        _filePath = filePath;
    }

    public T? Load()
    {
        var empty = default(T);
        if (File.Exists(_filePath) == false) return empty;
        using var stream = new StreamReader(File.Open(_filePath, FileMode.Open));
        var text = stream.ReadToEnd();
        return JsonSerializer.Deserialize<T>(text) ?? empty;
    }

    public void Save(T data)
    {
        var mode = FileMode.OpenOrCreate;
        if (File.Exists(_filePath)) mode = FileMode.Truncate;
        var text = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        using var stream = new StreamWriter(File.Open(_filePath, mode));
        stream.Write(text);
    }
}