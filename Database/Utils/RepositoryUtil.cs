using Database.Core;

namespace Database.Utils;

public class RepositoryUtil
{
    public static Type? GetCurrentRepositoryType(Type entityType)
    {
        var dbContextType = typeof(DbContext);
        var subclasses = TypesUtil.GetInheritOf(dbContextType);
        var repos = GetAllRepositories(subclasses);
        foreach (var repo in repos)
        {
            var generics = repo.GetGenericArguments().ToList();
            var needGenerics = generics.FirstOrDefault(x => x.Name == entityType.Name);
            if (needGenerics != null)
                return repo;
        }
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