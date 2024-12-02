namespace Database.Events;

public class SuccessEvents
{
    public delegate void Operation(object data);

    public event Operation Created;
    public event Operation Removed;

    public void OnCreated(object data)
    {
        Created?.Invoke(data);
    }

    public void OnRemoved(object data)
    {
        Removed?.Invoke(data);
    }
}