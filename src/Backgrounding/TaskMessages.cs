using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Maui.Extender.Backgrounding;

public class StartLongRunningTaskMessage : ValueChangedMessage<string>
{
    public StartLongRunningTaskMessage(string value = default!) : base(value)
    {
    }
}

public class StopLongRunningTaskMessage : ValueChangedMessage<string>
{
    public StopLongRunningTaskMessage(string value = default!) : base(value)
    {
    }
}
