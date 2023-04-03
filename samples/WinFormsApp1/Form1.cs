using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1(IServer server)    // 注入 IServer 服务，获取 Web 启动地址/端口
    {
        InitializeComponent();

        webview.Dock = DockStyle.Fill;
        webview.Source = new Uri(server.GetServerAddress());
    }
}