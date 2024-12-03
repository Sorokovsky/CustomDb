namespace Database.Indexing;

public class IndexUnit
{
    public IndexUnit(Type type, string fieldName)
    {
        Type = type;
        FieldName = fieldName;
    }

    public Type Type { get; private set; }

    public string FieldName { get; private set; }
}