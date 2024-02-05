using Android.Content;

namespace Maui.Extender.Backgrounding
{
    public class BackgroundAggregator
    {
        public static void Init(ContextWrapper context)
        {
            WeakReferenceMessenger.Default.Register<StartLongRunningTaskMessage>(context, (r, m) =>
            {
                var intent = new Intent(context, typeof(MauiBackgroundService));
                context.StartService(intent);
            });

            WeakReferenceMessenger.Default.Register<StopLongRunningTaskMessage>(context, (r, m) =>
            {
                var intent = new Intent(context, typeof(MauiBackgroundService));
                context.StopService(intent);
            });
        }
    }
}