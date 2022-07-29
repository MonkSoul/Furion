using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furion.Xunit;

/// <summary>
/// 测试配置启动项
/// </summary>
public class TestStartup : XunitTestFramework
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="messageSink"></param>
    public TestStartup(IMessageSink messageSink)
        : base(messageSink)
    {
    }

    /// <summary>
    /// 创建单元测试框架执行器
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <returns></returns>
    protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
    {
        return new XunitTestFrameworkExecutorWithAssemblyFixture(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
    }
}