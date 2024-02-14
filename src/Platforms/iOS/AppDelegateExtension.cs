using UIKit;

namespace Maui.Extender.Backgrounding;


public class BackgroundAggregator
{
    public static void Init(IUIApplicationDelegate appDelegate)
    {
        WeakReferenceMessenger.Default.Register<StartLongRunningTaskMessage>(appDelegate,
                                                                             (r, m) => MauiBackgroundService.Instance.Start());

        WeakReferenceMessenger.Default.Register<StopLongRunningTaskMessage>(appDelegate,
                                                                            (r, m) => MauiBackgroundService.Instance.Stop());
    }
}