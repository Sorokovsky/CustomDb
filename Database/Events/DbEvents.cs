namespace Database.Events;

public class DbEvents
{
    public delegate void ErrorOperation(string message);

    public delegate void Group(int count);

    public delegate void Operation(object data);

    public event Operation Created;

    public event Operation PreCreated;

    public event Operation Removed;

    public event Group Sorted;

    public event ErrorOperation NotRemoved;

    public event ErrorOperation NotSorted;

    public event Operation Updated;

    public event ErrorOperation NotUpdated;

    public void OnCreated(object data)
    {
        Created?.Invoke(data);
    }

    public void OnPreCreated(object data)
    {
        PreCreated?.Invoke(data);
    }

    public void OnRemoved(object data)
    {
        Removed?.Invoke(data);
    }

    public void OnNotRemoved(string message)
    {
        NotRemoved?.Invoke(message);
    }

    public void OnSorted(int count)
    {
        Sorted?.Invoke(count);
    }

    public void OnNotSorted(string message)
    {
        NotSorted?.Invoke(message);
    }

    public void OnUpdated(object data)
    {
        Updated?.Invoke(data);
    }

    public void OnNotUpdated(string message)
    {
        NotUpdated?.Invoke(message);
    }
}