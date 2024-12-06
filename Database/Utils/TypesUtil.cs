namespace Database.Utils;

public static class TypesUtil
{
    public static List<Type> Types => GetAllTypes();

    public static bool IsNumericType(Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16
                or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
            _ => false
        };
    }

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