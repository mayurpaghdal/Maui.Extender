namespace Maui.Extender.Backgrounding;

public class MauiBackgroundSharedService
{
    private static MauiBackgroundSharedService _instance;
    private static bool _isRunning;

    static MauiBackgroundSharedService()
    {
    }

    private MauiBackgroundSharedService()
    {
    }

    /// <summary>
    /// Single Instance of XamBackgroundService
    /// </summary>
    public static MauiBackgroundSharedService Instance { get; } = _instance ?? new();


    /// <summary>
    /// Start the execution of background service
    /// </summary>
    public void Start()
    {
        if (_isRunning) return;
        BackgroundTaskService.Instance.StartJob();
        _isRunning = true;
    }

    /// <summary>
    /// Stop the execution of background service
    /// </summary>
    public void Stop()
    {
        _isRunning = false;
        BackgroundTaskService.Instance.StopJob();
    }
}
