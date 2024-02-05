using Android.App;
using Android.Content;
using Android.OS;

namespace Maui.Extender.Backgrounding;

[Service]
public class MauiBackgroundService : Service
{
    private static bool _isRunning;

    public override IBinder? OnBind(Intent? intent)
    {
        return default!;
    }

    public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
    {
        if (!_isRunning)
        {
            //RUNNING TASK
            BackgroundAggregatorService.Instance.Start();

            _isRunning = true;
        }

        return StartCommandResult.Sticky;
    }

    public override void OnDestroy()
    {
        _isRunning = false;
        BackgroundAggregatorService.Instance.Stop();

        base.OnDestroy();
    }
}