using System.Reflection;
using Database.Contracts;
using Database.Core;
using Database.Relations.Keys;
using Database.Relations.Keys.Generators;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    private readonly IKeyGenerator _keyGenerator;

    public PrimaryKeyAttribute()
    {
        _keyGenerator = new IncrementalKey();
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
        property.SetValue(entity, _keyGenerator.NewKey);
    }
}