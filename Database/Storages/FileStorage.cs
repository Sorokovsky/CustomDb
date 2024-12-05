using System.Text.Json;
using Database.Contracts;

namespace Database.Storages;

public class FileStorage<T> : IStorage<T> where T : class
{
    private readonly string _filePath;
    private readonly string _folder;

    public FileStorage(string folder, string filePath)
    {
        _folder = folder;
        _filePath = $"{_folder}/{filePath}";
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
        if (Directory.Exists(_folder) == false) Directory.CreateDirectory(_folder);
        var mode = FileMode.OpenOrCreate;
        if (File.Exists(_filePath)) mode = FileMode.Truncate;
        var text = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        using var stream = new StreamWriter(File.Open(_filePath, mode));
        stream.Write(text);
    }

    public static T? LoadFromFile(string folder, string filePath)
    {
        var storage = new FileStorage<T>(folder, filePath);
        return storage.Load();
    }

    public static void SaveToFile(string folder, string filePath, T data)
    {
        var storage = new FileStorage<T>(folder, filePath);
        storage.Save(data);
    }
}