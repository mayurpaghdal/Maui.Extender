namespace Maui.Extender.Backgrounding;

public partial class BackgroundAggregatorService
{

    /* Unmerged change from project 'Maui.Extender (net8.0-ios)'
    Before:
        private static BackgroundAggregatorService _instance;

        internal static readonly CompositeDisposable EventSubscriptions = [];
    After:
        private static BackgroundAggregatorService _instance;

        internal static readonly CompositeDisposable EventSubscriptions = [];
    */
    private static BackgroundAggregatorService _instance;

    internal static readonly CompositeDisposable EventSubscriptions = [];
    private static readonly Dictionary<string, IPeriodicTask> _schedules = [];

    static BackgroundAggregatorService()
    {
    }

    private BackgroundAggregatorService()
    {
    }

    public static void Add<T>(Func<T> schedule) where T : IPeriodicTask
    {
        var typeName = schedule.GetType().GetGenericArguments()[0]?.Name;

        if (typeName != null && !_schedules.ContainsKey(typeName))
            _schedules.Add(typeName, schedule());
    }

    public static void StartBackgroundService()
    {
        var message = new StartLongRunningTaskMessage();
        WeakReferenceMessenger.Default.Send(message);
    }

    public static void StopBackgroundService()
    {
        var message = new StopLongRunningTaskMessage();
        WeakReferenceMessenger.Default.Send(message);
    }

    public static BackgroundAggregatorService Instance { get; } = _instance ?? (_instance = new BackgroundAggregatorService());

    public void Start()
    {
        foreach (var schedule in _schedules)
        {
            var observable = SyncRepeatObservable(schedule.Value);
            EventSubscriptions.Add(observable);
        }
    }

    public void Stop()
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
