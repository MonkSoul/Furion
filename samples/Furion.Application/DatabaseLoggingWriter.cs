using Furion.Logging;

namespace Furion.Application;

public class DatabaseLoggingWriter : IDatabaseLoggingWriter
{
    public void Write(LogMessage logMsg, bool flush)
    {
        //Console.WriteLine(logMsg.Message);
    }
}