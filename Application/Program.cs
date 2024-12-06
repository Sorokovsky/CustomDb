using Application.Entities;

namespace Application;

public static class Program
{
    public static void Main()
    {
        var database = DbContext.Instance;
        database.Entities.Add(new BaseEntity());
        database.Entities.Add(new BaseEntity());
        database.Entities.Add(new BaseEntity());
    }
}