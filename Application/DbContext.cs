namespace Application;

public class DbContext : Database.Core.DbContext
{
    private static DbContext? _instance;

    private DbContext()
    {
    }

    public static DbContext Singleton()
    {
        if (_instance == null) _instance = new DbContext();
        return _instance;
    }
}