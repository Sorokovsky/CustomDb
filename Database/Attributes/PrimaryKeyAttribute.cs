using System.Reflection;
using Database.Contracts;
using Database.Core;
using Database.Relations.Keys;
using Database.Relations.Keys.Generators;
using Database.Utils;

namespace Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    private readonly IKeyGenerator _keyGenerator;

    public PrimaryKeyAttribute()
    {
        _keyGenerator = new IncrementalKey();
        DbContext.Events.Created += OnCreatedAt;
    }

    ~PrimaryKeyAttribute()
    {
        DbContext.Events.Created -= OnCreatedAt;
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

    private void OnCreatedAt(object entity)
    {
        var newId = _keyGenerator.NewKey;
        var repo = RepositoryUtil.GetCurrentRepository(entity.GetType());
    }
}