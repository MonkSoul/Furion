namespace WinFormsApp1;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Serve.RunNative(RunOptions.Default);

        ApplicationConfiguration.Initialize();
        Application.Run(Native.CreateInstance<Form1>());
    }
}