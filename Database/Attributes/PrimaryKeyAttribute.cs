using System.Reflection;
using Database.Contracts;
using Database.Core;
using Database.Relations.Keys;
using Database.Relations.Keys.Generators;
using Database.Storages;
using Database.Utils;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    private IKeyGenerator _keyGenerator;

    public override void Construct()
    {
        var property = ConvertMemberToProperty();
        if (TypesUtil.IsNumericType(property.PropertyType) == false)
            throw new ArgumentException($"The type {property.PropertyType} is not a numeric type");
        var lastId =  FileStorage<string>.LoadFromFile("database/lastKeys", PrepareFilePath()) ?? "0";
        var lastKey = int.Parse(lastId);
        _keyGenerator = new IncrementalKey(lastKey);
        DbContext.Events.PreCreated += OnPreCreatedAt;
    }

    ~PrimaryKeyAttribute()
    {
        DbContext.Events.PreCreated -= OnPreCreatedAt;
    }

    public override void Process()
    {
        var property = ConvertMemberToProperty();
        var key = Key.CreatePrimaryKey(property.Name, ParentType!.Name);
        var manager = new KeysManager();
        manager.TryAddKey(key);
    }

    private PropertyInfo ConvertMemberToProperty()
    {
        return (PropertyInfo)Member!;
    }

    private void OnPreCreatedAt(object entity)
    {
        var property = ConvertMemberToProperty();
        var newKey = _keyGenerator.NewKey;
        property.SetValue(entity, newKey);
        FileStorage<string>.SaveToFile("database/lastKeys", PrepareFilePath(), $"{newKey}");
    }

    private string PrepareFilePath()
    {
        return $"{ParentType?.Name}.{Member?.Name}";
    }
}