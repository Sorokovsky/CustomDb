using Database.Events;

namespace Database.Core;

public abstract class DbContext
{
    public static DbEvents Events { get; private set; } = new();
}