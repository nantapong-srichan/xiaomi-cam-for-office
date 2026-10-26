using Avalonia.Controls;
using Avalonia.Interactivity;
using LibVLCSharp.Shared;
using System.Diagnostics;

namespace OfficeCamera;

public partial class MainWindow : Window
{
    private readonly LibVLC? _libVlc;
    private readonly MediaPlayer? _classroomPlayer;
    private readonly MediaPlayer? _livingroomPlayer;
    private Media? _classroomMedia;
    private Media? _livingroomMedia;

    public MainWindow()
    {
        InitializeComponent();
        if (Program.VlcAvailable)
        {
            _libVlc = new LibVLC();
            _classroomPlayer = new MediaPlayer(_libVlc);
            _livingroomPlayer = new MediaPlayer(_libVlc);
            ClassroomVideoView.MediaPlayer = _classroomPlayer;
            LivingroomVideoView.MediaPlayer = _livingroomPlayer;
        }
    }

    private static string HlsUrl(string name) =>
        $"http://127.0.0.1:1984/api/stream.m3u8?src={name}";

    private void ConnectBoth(object? sender, RoutedEventArgs e)
    {
        var classroom = Play("classroom", _classroomPlayer, ref _classroomMedia);
        var livingroom = Play("livingroom", _livingroomPlayer, ref _livingroomMedia);
        Status.Text = classroom && livingroom ? "กำลังเปิดภาพทั้งสองกล้อง" : "เปิดได้บางกล้อง — ตรวจสอบ go2rtc";
    }

    private void ConnectClassroom(object? sender, RoutedEventArgs e) =>
        Status.Text = Play("classroom", _classroomPlayer, ref _classroomMedia)
            ? "กำลังเปิดกล้องห้องเรียน" : "เปิดกล้องห้องเรียนไม่สำเร็จ";

    private void ConnectLivingroom(object? sender, RoutedEventArgs e) =>
        Status.Text = Play("livingroom", _livingroomPlayer, ref _livingroomMedia)
            ? "กำลังเปิดกล้องห้องนั่ง" : "เปิดกล้องห้องนั่งไม่สำเร็จ";

    private bool Play(string name, MediaPlayer? player, ref Media? currentMedia)
    {
        var url = HlsUrl(name);
        if (!Program.VlcAvailable || player is null || _libVlc is null)
        {
            // The current LibVLC Mac package does not include an arm64 native build.
            // Open go2rtc's native WebUI as a reliable playback fallback.
            Process.Start(new ProcessStartInfo("open", "http://127.0.0.1:1984/") { UseShellExecute = false });
            return true;
        }

        try
        {
            currentMedia?.Dispose();
            currentMedia = new Media(_libVlc, new Uri(HlsUrl(name)));
            return player.Play(currentMedia);
        }
        catch
        {
            return false;
        }
    }

    private void ScanNetwork(object? sender, RoutedEventArgs e) =>
        Status.Text = "ฟังก์ชันสแกนเครือข่ายจะเชื่อมกับ service ในขั้นถัดไป";

    protected override void OnClosed(EventArgs e)
    {
        _classroomPlayer?.Stop();
        _livingroomPlayer?.Stop();
        _classroomMedia?.Dispose();
        _livingroomMedia?.Dispose();
        _classroomPlayer?.Dispose();
        _livingroomPlayer?.Dispose();
        _libVlc?.Dispose();
        base.OnClosed(e);
    }
}
