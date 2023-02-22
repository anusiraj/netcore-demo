namespace NETCoreDemo.Services;

public class RequestCounterService : ICounterService
{
    private int _counter; 

    public int CurrentValue
    {
        get => _counter;
    }

    public void Increase()
    {
        Interlocked.Increment(ref _counter);
    }
}