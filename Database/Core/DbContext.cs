using Database.Events;

namespace Database.Core;

public abstract class DbContext
{
    public static SuccessEvents SuccessEvents { get; private set; } = new();
}