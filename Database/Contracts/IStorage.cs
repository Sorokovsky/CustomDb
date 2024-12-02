namespace Database.Contracts;

public interface IStorage<T> where T : class
{
    public T? Load();

    public void Save(T data);
}