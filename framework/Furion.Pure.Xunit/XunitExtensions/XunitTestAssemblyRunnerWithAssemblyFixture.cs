// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furion.Xunit;

/// <summary>
/// 单元测试程序集调用器
/// </summary>
public class XunitTestAssemblyRunnerWithAssemblyFixture : XunitTestAssemblyRunner
{
    private readonly Dictionary<Type, object> assemblyFixtureMappings = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="testAssembly"></param>
    /// <param name="testCases"></param>
    /// <param name="diagnosticMessageSink"></param>
    /// <param name="executionMessageSink"></param>
    /// <param name="executionOptions"></param>
    public XunitTestAssemblyRunnerWithAssemblyFixture(ITestAssembly testAssembly,
                                                      IEnumerable<IXunitTestCase> testCases,
                                                      IMessageSink diagnosticMessageSink,
                                                      IMessageSink executionMessageSink,
                                                      ITestFrameworkExecutionOptions executionOptions)
        : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
    {
    }

    /// <summary>
    /// 单元测试程序集调用时触发
    /// </summary>
    /// <returns></returns>
    protected async override Task AfterTestAssemblyStartingAsync()
    {
        // 让测试程序集回归初始状态
        await base.AfterTestAssemblyStartingAsync();

        // 查找所有程序集中是否有类型定义了 [AssemblyFixture] 特性，如果有，则实例化并注入到所有单元测试实例中
        Aggregator.Run(() =>
        {
            var fixturesAttrs = ((IReflectionAssemblyInfo)TestAssembly.Assembly).Assembly
                                                                                .GetCustomAttributes(typeof(AssemblyFixtureAttribute), false)
                                                                                .Cast<AssemblyFixtureAttribute>()
                                                                                .ToList();

            // 创建 [AssemblyFixture] 配置的类型实例并注入到所有程序集单元测试实例中
            foreach (var fixtureAttr in fixturesAttrs)
            {
                assemblyFixtureMappings[fixtureAttr.FixtureType] = Activator.CreateInstance(fixtureAttr.FixtureType);
            }
        });
    }

    /// <summary>
    /// 单元测试程序集销毁时触发
    /// </summary>
    /// <returns></returns>
    protected override Task BeforeTestAssemblyFinishedAsync()
    {
        // Make sure we clean up everybody who is disposable, and use Aggregator.Run to isolate Dispose failures
        foreach (var disposable in assemblyFixtureMappings.Values.OfType<IDisposable>())
            Aggregator.Run(disposable.Dispose);

        return base.BeforeTestAssemblyFinishedAsync();
    }

    /// <summary>
    /// 执行多个测试实例
    /// </summary>
    /// <param name="messageBus"></param>
    /// <param name="testCollection"></param>
    /// <param name="testCases"></param>
    /// <param name="cancellationTokenSource"></param>
    /// <returns></returns>
    protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus,
                                                               ITestCollection testCollection,
                                                               IEnumerable<IXunitTestCase> testCases,
                                                               CancellationTokenSource cancellationTokenSource)
    {
        return new XunitTestCollectionRunnerWithAssemblyFixture(assemblyFixtureMappings, testCollection, testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
    }
}