using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Web.WebView2.Core;
using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow(IServer server)   // 注入 IServer 服务，获取 Web 启动地址/端口
    {
        InitializeComponent();
        webview.Source = new Uri(server.GetServerAddress());

        // 添加 WebView 右键菜单：在浏览器中打开
        webview.CoreWebView2InitializationCompleted += (a, c) =>
        {
            webview.CoreWebView2.ContextMenuRequested += (sender, args) =>
            {
                var newItem = webview.CoreWebView2.Environment.CreateContextMenuItem(
                    "在浏览器中打开", null, CoreWebView2ContextMenuItemKind.Command);

                newItem.CustomItemSelected += (send, ex) =>
                {
                    System.Diagnostics.Process.Start("explorer.exe", args.ContextMenuTarget.PageUri);
                };

                args.MenuItems.Insert(args.MenuItems.Count, newItem);
            };
        };
    }
}