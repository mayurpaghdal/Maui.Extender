using UIKit;

namespace Maui.Extender.Backgrounding
{
    public class MauiBackgroundService
    {
        private static nint _taskId;
        private static MauiBackgroundService _instance;
        private static bool _isRunning;

        static MauiBackgroundService()
        {
        }

        private MauiBackgroundService()
        {
        }

        /// <summary>
        /// Single Instance of XamBackgroundService
        /// </summary>
        public static MauiBackgroundService Instance { get; } = _instance ??= new();

        /// <summary>
        /// Start the execution of background service
        /// </summary>
        public void Start()
        {
            if (_isRunning) return;

            //We only have 3 minutes in the background service as per iOS 9
            _taskId = UIApplication.SharedApplication.BeginBackgroundTask(nameof(StartLongRunningTaskMessage), Stop);
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
            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }
    }
}