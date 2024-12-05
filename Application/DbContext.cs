using Application.Entities;
using Database.Core;

namespace Application;

public class DbContext : Database.Core.DbContext
{
    private static DbContext? _instance;

    private DbContext()
    {
    }

    public static DbContext Instance => Singleton();

    public Repository<BaseEntity> Entities { get; } = new();

    private static DbContext Singleton()
    {
        if (_instance == null) _instance = new DbContext();
        return _instance;
    }
}