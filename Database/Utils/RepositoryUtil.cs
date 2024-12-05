using Database.Core;

namespace Database.Utils;

public class RepositoryUtil
{
    public static Repository<dynamic> GetCurrentRepository(Type entityType)
    {
        var dbContextType = typeof(DbContext);
        var subclasses = TypesUtil.GetInheritOf(dbContextType);
        var repos = GetAllRepositories(subclasses);
        repos.ForEach(x => Console.WriteLine(x.Name));
        return null;
    }

    public static List<Type> GetAllRepositories(List<Type> contextTypes)
    {
        var repos = new List<Type>();
        contextTypes
            .ForEach(x => repos.AddRange(x
                .GetProperties()
                .Where(x => x.PropertyType.Name == typeof(Repository<dynamic>).Name)
                .Select(x => x.PropertyType))
            );
        return repos;
    }
}