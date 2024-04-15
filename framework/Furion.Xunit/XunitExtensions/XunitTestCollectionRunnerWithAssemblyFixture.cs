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

using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furion.Xunit;

/// <summary>
/// 单元测试多个测试实例调用器
/// </summary>
public class XunitTestCollectionRunnerWithAssemblyFixture : XunitTestCollectionRunner
{
    private readonly Dictionary<Type, object> assemblyFixtureMappings;
    private readonly IMessageSink diagnosticMessageSink;

    /// <summary>
    /// 创建服务作用域
    /// </summary>
    private IServiceScope serviceScope;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assemblyFixtureMappings"></param>
    /// <param name="testCollection"></param>
    /// <param name="testCases"></param>
    /// <param name="diagnosticMessageSink"></param>
    /// <param name="messageBus"></param>
    /// <param name="testCaseOrderer"></param>
    /// <param name="aggregator"></param>
    /// <param name="cancellationTokenSource"></param>
    public XunitTestCollectionRunnerWithAssemblyFixture(Dictionary<Type, object> assemblyFixtureMappings,
                                                        ITestCollection testCollection,
                                                        IEnumerable<IXunitTestCase> testCases,
                                                        IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        ITestCaseOrderer testCaseOrderer,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource)
        : base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
    {
        this.assemblyFixtureMappings = assemblyFixtureMappings;
        this.diagnosticMessageSink = diagnosticMessageSink;
    }

    /// <summary>
    /// 单元测试实例测试时触发
    /// </summary>
    /// <param name="testClass"></param>
    /// <param name="class"></param>
    /// <param name="testCases"></param>
    /// <returns></returns>
    protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
    {
        // 定义单元测试实例所有构造函数参数实例集合
        var combinedFixtures = new Dictionary<Type, object>(assemblyFixtureMappings);
        foreach (var kvp in CollectionFixtureMappings)
        {
            combinedFixtures[kvp.Key] = kvp.Value;
        }

        // 获取测试实例构造函数
        var constructors = @class.Type.GetConstructors();

        // 不允许多个构造函数
        if (constructors.Length > 1) throw new InvalidProgramException("More than one constructor declaration found.");

        // 如果声明了构造函数
        if (constructors.Length > 0)
        {
            // 获取构造函数参数
            var parameters = constructors[0]
                .GetParameters()
                .Where(u => !u.ParameterType.Assembly.GetName().Name.StartsWith("xunit."));

            // 创建服务作用域
            serviceScope = App.RootServices.CreateScope();

            // 循环所有接口参数并进行服务解析
            foreach (var parameter in parameters)
            {
                var serviceType = parameter.ParameterType;
                object serviceInstance;

                // 获取服务注册生命周期
                var serviceLifetime = App.GetServiceLifetime(serviceType);
                // 如果是单例，直接从根服务解析
                if (serviceLifetime == ServiceLifetime.Singleton)
                {
                    serviceInstance = App.RootServices.GetService(serviceType);
                }
                // 否则通过作用域解析
                else
                {
                    serviceInstance = serviceScope.ServiceProvider.GetService(serviceType);
                }

                combinedFixtures.TryAdd(serviceType, serviceInstance);
            }
        }

        // 创建单元测试实例
        return new XunitTestClassRunner(testClass, @class, testCases, diagnosticMessageSink, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource, combinedFixtures).RunAsync();
    }

    /// <summary>
    /// 单元测试实例销毁时触发
    /// </summary>
    /// <returns></returns>
    protected override Task BeforeTestCollectionFinishedAsync()
    {
        serviceScope?.Dispose();
        return base.BeforeTestCollectionFinishedAsync();
    }
}