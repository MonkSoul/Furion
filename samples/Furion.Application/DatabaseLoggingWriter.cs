using Furion.Logging;

namespace Furion.Application;

public class DatabaseLoggingWriter : IDatabaseLoggingWriter
{
    public Task WriteAsync(LogMessage logMsg, bool flush)
    {
        //Console.WriteLine(logMsg.Message);
        return Task.CompletedTask;
    }
}