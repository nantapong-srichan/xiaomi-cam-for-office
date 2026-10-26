using Avalonia;
using LibVLCSharp.Shared;

namespace OfficeCamera;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Core.Initialize();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
}
