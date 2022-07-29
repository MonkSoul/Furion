// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
                combinedFixtures.TryAdd(parameter.ParameterType, serviceScope.ServiceProvider.GetService(parameter.ParameterType));
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