namespace Application;

public static class Program
{
    public static void Main()
    {
        var database = DbContext.Singleton();
    }
}