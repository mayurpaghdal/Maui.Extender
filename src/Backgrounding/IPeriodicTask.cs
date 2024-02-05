namespace Maui.Extender.Backgrounding;

public interface IPeriodicTask
{
    TimeSpan Interval { get; }
    Task<bool> StartJob();
}
