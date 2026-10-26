using Avalonia;
using LibVLCSharp.Shared;

namespace OfficeCamera;

internal static class Program
{
    public static bool VlcAvailable { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            Core.Initialize();
            VlcAvailable = true;
        }
        catch
        {
            // Some Apple Silicon environments do not have a matching LibVLC native build.
            // The UI remains usable and falls back to the system browser for HLS playback.
            VlcAvailable = false;
        }
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
}
