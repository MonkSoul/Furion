using System.Windows;

namespace WpfApp1;

public partial class App : Application
{
    public App()
    {
        // Serve.RunNative(RunOptions.Default);    // 默认 5000 端口，如果出现占用，推荐使用下面的方式
        Serve.RunNative(RunOptions.Default, Serve.IdleHost.Urls); // 随机端口
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Native.CreateInstance<MainWindow>().Show();
        base.OnStartup(e);
    }
}