using System.Windows;

namespace WpfApp1;

public partial class App : Application
{
    public App()
    {
        Serve.RunNative(RunOptions.Default);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Native.CreateInstance<MainWindow>().Show();
        base.OnStartup(e);
    }
}