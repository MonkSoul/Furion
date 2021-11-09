using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

// 配置启动类类型
[assembly: TestFramework("Furion.UnitTests.Startup", "Furion.UnitTests")]

namespace Furion.UnitTests;

/// <summary>
/// 单元测试初始化类
/// </summary>
public sealed class Startup : XunitTestFramework
{
    public Startup(IMessageSink messageSink) : base(messageSink)
    {
        var services = Inject.Create();
        services.Build();
    }
}
