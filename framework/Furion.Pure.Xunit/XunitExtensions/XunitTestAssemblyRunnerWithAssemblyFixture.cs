// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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