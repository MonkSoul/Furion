using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework("Furion.UnitTests._", "Furion.UnitTests")]
namespace Furion.UnitTests
{
    public sealed class Initialize : XunitTestFramework
    {
        public Initialize(IMessageSink messageSink) : base(messageSink)
        {
            var services = Inject.Create();
            services.Build();
        }
    }
}
