using Windows.ApplicationModel.Background;

namespace Maui.Extender.Backgrounding;


public class BackgroundAggregator
{
    private static string BackServiceName = "BackgroundService";

    public static void Init(MauiWinUIApplication page)
    {
        WeakReferenceMessenger.Default.Register<StartLongRunningTaskMessage>(page, async (r, message) =>
            {
                var access = await BackgroundExecutionManager.RequestAccessAsync();

                switch (access)
                {
                    case BackgroundAccessStatus.Unspecified:
                        return;
                    case BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity:
                        return;
                    case BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity:
                        return;
                    case BackgroundAccessStatus.Denied:
                        return;
                }

                var task = new BackgroundTaskBuilder
                {
                    Name = BackServiceName,
                };

                var trigger = new ApplicationTrigger();
                task.SetTrigger(trigger);

                //var condition = new SystemCondition(SystemConditionType.InternetAvailable);
                task.Register();

                await trigger.RequestAsync();
            });

        WeakReferenceMessenger.Default.Register<StopLongRunningTaskMessage>(page, (r, message) =>
            {
                var tasks = BackgroundTaskRegistration.AllTasks;
                foreach (var task in tasks)
                {
                    // You can check here for the name
                    string name = task.Value.Name;
                    if (name == BackServiceName)
                    {
                        task.Value.Unregister(true);
                    }
                }
            });
    }
}
