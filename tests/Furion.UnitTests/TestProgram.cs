using Furion.Xunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Xunit.Abstractions;

[assembly: TestFramework("Furion.UnitTests.TestProgram", "Furion.UnitTests")]

namespace Furion.UnitTests;

public class TestProgram : TestStartup
{
    public TestProgram(IMessageSink messageSink) : base(messageSink)
    {
        Serve.RunNative(services =>
        {
            services.AddRemoteRequest();
        });
    }
}