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

            return builder;
        }
    }
}
