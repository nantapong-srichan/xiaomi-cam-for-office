using Avalonia.Controls;
using Avalonia.Interactivity;

namespace OfficeCamera;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void ConnectStream(object? sender, RoutedEventArgs e)
    {
        var url = StreamUrl.Text?.Trim();
        StreamStatus.Text = string.IsNullOrWhiteSpace(url)
            ? "กรุณาระบุ URL จาก go2rtc ก่อน"
            : $"เตรียมเชื่อมต่อ: {url}";
    }

    private void ScanNetwork(object? sender, RoutedEventArgs e) =>
        Status.Text = "ฟังก์ชันสแกนเครือข่ายจะเชื่อมกับ service ในขั้นถัดไป";
}
