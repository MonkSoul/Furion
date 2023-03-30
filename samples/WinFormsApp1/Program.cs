namespace WinFormsApp1;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        // Serve.RunNative(RunOptions.Default);    // 默认 5000 端口，如果出现占用，推荐使用下面的方式
        Serve.RunNative(RunOptions.Default, Serve.IdleHost.Urls);   // 随机端口

        ApplicationConfiguration.Initialize();
        Application.Run(Native.CreateInstance<Form1>());
    }
}