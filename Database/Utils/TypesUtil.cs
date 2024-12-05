namespace Database.Utils;

public static class TypesUtil
{
    public static List<Type> Types => GetAllTypes();

    private static List<Type> GetAllTypes()
    {
        var types = new List<Type>();
        AppDomain.CurrentDomain.GetAssemblies()
            .Select(assembly => assembly.GetTypes()
            )
            .ToList()
            .ForEach(x => types.AddRange(x));
        return types;
    }

    public static List<Type> GetInheritOf(Type baseType)
    {
        return Types.Where(x => x.IsAbstract == false && x.IsSubclassOf(baseType)).ToList();
    }
}