using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow(IServer server)   // 注入 IServer 服务，获取 Web 启动地址/端口
    {
        InitializeComponent();
        webview.Source = new Uri(server.GetServerAddress());
    }
}