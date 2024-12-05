using Database.Contracts;

namespace Database.Relations.Keys.Generators;

public class IncrementalKey : IKeyGenerator
{
    private int _currentKey;

    public IncrementalKey(int initial = 0)
    {
        _currentKey = initial;
    }

    public int NewKey => ++_currentKey;
}