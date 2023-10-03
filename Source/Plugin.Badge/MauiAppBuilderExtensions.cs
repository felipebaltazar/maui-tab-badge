

namespace Plugin.Badge.Abstractions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UserTabbedBadgeCompat(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers((handlers) =>
        {
#if ANDROID
            handlers.AddHandler(typeof(TabbedPage), typeof(Plugin.Badge.Droid.BadgedTabbedPageRenderer));
#elif IOS
            handlers.AddHandler(typeof(TabbedPage), typeof(Plugin.Badge.iOS.BadgedTabbedPageRenderer));
#endif
        });

        return builder;
    }
}
