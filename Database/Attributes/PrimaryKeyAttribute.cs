using System.Reflection;
using Database.Contracts;
using Database.Core;
using Database.Relations.Keys;
using Database.Relations.Keys.Generators;
using Database.Storages;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    private IKeyGenerator _keyGenerator;

    public override void Construct()
    {
        var lastId =  FileStorage<string>.LoadFromFile("database/lastKeys", ParentType?.Name!) ?? "0";
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
        FileStorage<string>.SaveToFile("database/lastKeys", $"{ParentType?.Name}", $"{newKey}");
    }
}