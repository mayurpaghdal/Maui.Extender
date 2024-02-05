namespace Maui.Extender.Backgrounding;

public partial class BackgroundTaskService
{
    private static BackgroundTaskService _instance;

    internal static readonly CompositeDisposable EventSubscriptions = [];
    private static readonly Dictionary<string, IPeriodicTask> _schedules = [];

    static BackgroundTaskService()
    {
    }

    private BackgroundTaskService()
    {
    }

    public static void Add<T>(Func<T> schedule) where T : IPeriodicTask
    {
        var typeName = schedule.GetType().GetGenericArguments()[0]?.Name;

        if (typeName != null && !_schedules.ContainsKey(typeName))
            _schedules.Add(typeName, schedule());
    }

    public static void Start()
    {
        var message = new StartLongRunningTaskMessage();
        WeakReferenceMessenger.Default.Send(message);
    }

    public static void Stop()
    {
        var message = new StopLongRunningTaskMessage();
        WeakReferenceMessenger.Default.Send(message);
    }

    public static BackgroundTaskService Instance { get; } = _instance ?? (_instance = new BackgroundTaskService());

    public void StartJob()
    {
        foreach (var schedule in _schedules)
        {
            var observable = SyncRepeatObservable(schedule.Value);
            EventSubscriptions.Add(observable);
        }
    }

    public void StopJob()
    {
        EventSubscriptions.Clear();
    }

    public void Clear()
    {
        EventSubscriptions.Clear();
        _schedules.Clear();
    }

    private static IDisposable SyncRepeatObservable(IPeriodicTask schedule)
    {
        return Observable.FromAsync(schedule.StartJob)
                         .Delay(schedule.Interval)
                         .Repeat()
                         .TakeWhile(e => e)
                         .Subscribe();
    }
}
