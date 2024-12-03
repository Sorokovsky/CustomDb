namespace Database.Indexing;

public class Index
{
    public Index(IndexUnit dependency, IndexUnit dependsOn)
    {
        Dependency = dependency;
        DependsOn = dependsOn;
    }

    public IndexUnit Dependency { get; private set; }

    public IndexUnit DependsOn { get; private set; }
}