using Furion.Xunit;
using System;
using Xunit;
using Xunit.Abstractions;

[assembly: TestFramework("Furion.UnitTests.Startup", "Furion.UnitTests")]

namespace Furion.UnitTests;

public class Startup : TestStartup
{
    public Startup(IMessageSink messageSink) : base(messageSink)
    {
        Serve.Run(silence: true);
    }
}