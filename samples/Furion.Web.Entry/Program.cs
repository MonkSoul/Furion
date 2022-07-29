using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

Serve.Run(RunOptions.Default.ConfigureBuilder(builder =>
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole(options =>
    {
        options.FormatterName = "custom_format";
    }).AddConsoleFormatter<YourForamt, ConsoleFormatterOptions>();
}));

public class YourForamt : ConsoleFormatter
{
    public YourForamt() : base("custom_format")
    {
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
    {
        string message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);

        if (message is null)
        {
            return;
        }

        textWriter.Write("日志级别：" + logEntry.LogLevel + " ");
        textWriter.Write("其他叉叉：" + logEntry.Category + " ");

        textWriter.Write(message);
        textWriter.WriteLine();
    }
}