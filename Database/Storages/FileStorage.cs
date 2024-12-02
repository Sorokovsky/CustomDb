using System.Text.Json;
using Database.Contracts;

namespace Database.Storages;

public class FileStorage<T> : IStorage<T> where T : class
{
    private const string Folder = "database";
    private readonly string _filePath;

    public FileStorage(string filePath)
    {
        _filePath = $"{Folder}/{filePath}.dat";
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
        if (Directory.Exists(Folder) == false) Directory.CreateDirectory(Folder);
        var mode = FileMode.OpenOrCreate;
        if (File.Exists(_filePath)) mode = FileMode.Truncate;
        var text = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        using var stream = new StreamWriter(File.Open(_filePath, mode));
        stream.Write(text);
    }
}