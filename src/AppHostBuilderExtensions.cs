using Maui.Extender.Backgrounding;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.Extender
{
    // All the code in this file is included in all platforms.
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder UseMauiExtenders(this MauiAppBuilder builder)
        {
            builder.Services.Configure<IServiceCollection>(services =>
            {
                services.AddSingleton(Connectivity.Current);
                services.AddSingleton<IEventAggregator, EventAggregator>();
            });

            builder.ConfigureEffects(effects =>
             {
                 effects.Add<TouchRoutingEffect, TouchEffectPlatform>();
                 effects.Add<IconTintColorRoutingEffect, IconTintColorEffectPlatform>();
                 effects.Add<CommandsRoutingEffect, CommandEffectPlatform>();
             });

            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android =>

                    android.OnCreate((activity, savedInstanceState) =>
                    {
                        BackgroundAggregator.Init(activity);
                    })
                );
//#elif IOS
//                events.AddiOS(iOSbuilder =>

//                    iOSbuilder.FinishedLaunching((app, lanchOptions) =>
//                    {
//                        BackgroundAggregator.Init(app.Delegate);
//                        return false;
//                    })
//                );
#endif

            });

            return builder;
        }
    }
}
